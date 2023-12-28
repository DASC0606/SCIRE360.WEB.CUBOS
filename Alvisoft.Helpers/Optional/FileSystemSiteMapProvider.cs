using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Web.Caching;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Web;


namespace Alvisoft.Helpers
    {
    public class FileSystemSiteMapProvider : StaticSiteMapProvider
        {
        #region "Private Member Variables"
	private SiteMapNode _root;
	private StringDictionary _excludeFileList;
	private StringDictionary _excludeFolderList;
	private Char[] _listSeparator = { ',' };
	private string _rootUrl = "~/../secured/default.aspx";
	private string _rootTitle = "Home";
	private bool _useDefaultPageAsFolderUrl = true;
	private string _defaultPageName = "default.aspx";
		#endregion
	private CacheDependency _fsMonitor;

	#region "Public Properties"
	public string RootUrl {
		get { return _rootUrl; }
		set {
			_rootUrl = value;

			Refresh();
		}
	}

	public string RootTitle {
		get { return _rootTitle; }
		set {
			_rootTitle = value;

			Refresh();
		}
	}

	public string DefaultPageName {
		get { return _defaultPageName; }
		set {
			_defaultPageName = value;

			Refresh();
		}
	}

	public bool UseDefaultPageAsFolderUrl {
		get { return _useDefaultPageAsFolderUrl; }
		set {
			_useDefaultPageAsFolderUrl = value;

			Refresh();
		}
	}

	protected StringDictionary ExcludeFileList {
		get {
			if (_excludeFileList == null) {
				_excludeFileList = new StringDictionary();
			}

			return _excludeFileList;
		}
	}

	protected StringDictionary ExcludeFolderList {
		get {
			if (_excludeFolderList == null) {
				_excludeFolderList = new StringDictionary();
			}

			return _excludeFolderList;
		}
	}
	#endregion

	#region "Methods for Adding/Removing/Listing Files/Folders from Exclude List"
	public void AddFileToExcludeList(string file)
	{
		if (!ExcludeFileList.ContainsKey(file)) {
			ExcludeFileList.Add(file, string.Empty);

			Refresh();
		}
	}

	public void AddFolderToExcludeList(string folder)
	{
		if (!ExcludeFolderList.ContainsKey(folder)) {
			ExcludeFolderList.Add(folder, string.Empty);

			Refresh();
		}
	}

	public bool RemoveFileFromExcludeList(string file)
	{
		if (ExcludeFileList.ContainsKey(file)) {
			ExcludeFileList.Remove(file);

			Refresh();

			return true;
		} else {
			return false;
		}
	}

	public bool RemoveFolderFromExcludeList(string folder)
	{
		if (ExcludeFolderList.ContainsKey(folder)) {
			ExcludeFolderList.Remove(folder);

			Refresh();

			return true;
		} else {
			return false;
		}
	}

	public ICollection ExcludedFiles {
		get { return ExcludeFileList.Keys; }
	}

	public ICollection ExcludedFolders {
		get { return ExcludeFolderList.Keys; }
	}
	#endregion

	public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
	{
		base.Initialize(name, attributes);

		// See if there is a list of files to exclude
		if (!string.IsNullOrEmpty(attributes["excludeFiles"])) {
			ConvertToStringDictionary(ExcludeFileList, attributes["excludeFiles"]);
		}

		if (!string.IsNullOrEmpty(attributes["excludeFolders"])) {
			ConvertToStringDictionary(ExcludeFolderList, attributes["excludeFolders"]);
		}

		//Read in the rootUrl & rootTitle properties, if they exist
		if (!string.IsNullOrEmpty(attributes["rootUrl"]))
			RootUrl = attributes["rootUrl"];
		if (!string.IsNullOrEmpty(attributes["rootTitle"]))
			RootTitle = attributes["rootTitle"];

		//Read in the useDefaultPageAsFolderUrl and defaultPageName properties, if they exist
		if (!string.IsNullOrEmpty(attributes["useDefaultPageAsFolderUrl"]))
			UseDefaultPageAsFolderUrl = XmlConvert.ToBoolean(attributes["useDefaultPageAsFolderUrl"]);
		if (!string.IsNullOrEmpty(attributes["defaultPageName"]))
			DefaultPageName = attributes["defaultPageName"];
	}

	private void ConvertToStringDictionary(StringDictionary dict, string delimitedList)
	{
		string[] pieces = delimitedList.Split(_listSeparator);
		foreach (string piece in pieces) {
			dict.Add(HttpContext.Current.Server.MapPath(piece.Trim().ToLower()), string.Empty);
		}
	}

	public void Refresh()
	{
		_root = null;

		base.Clear();
	}

	public override System.Web.SiteMapNode BuildSiteMap()
	{
		//Need to lock to ensure thread safety, since multiple pages in the app
		//might be trying to call this method concurrently
		lock (this) {
			//See if a root has been defined
			if (_root != null) {
				//We have a root - but has the underlying file system been changed?
				if (!_fsMonitor.HasChanged) {
					//No change to FS, returned the cached root
					return _root;
				}

				//The file system has been changed since we've last called
				//BuildSiteMap - we need to rebuild the sitemap
			}

			//If we reach here, either we don't have a root or the file system has been changed
			//must build up the Site Map. Clear out the site map, if it already exists...
			Refresh();

			//Create a root node
			_root = CreateFolderNode(HttpContext.Current.Server.MapPath(RootUrl), RootUrl);
			_root.Title = RootTitle;

			//Establish the cache dependency
			_fsMonitor = new CacheDependency(HttpContext.Current.Server.MapPath("~/"));

			AddNode(_root);
			//Add the root to the site map

			//Recurse through the file system, adding nodes
			BuildSiteMapFromFileSystem(_root, "~/");

			return _root;
		}
	}

	protected void BuildSiteMapFromFileSystem(SiteMapNode parentNode, string folderPath)
	{
		//Determine the folder path for currentNode
		string folder = HttpContext.Current.Server.MapPath(folderPath);

		//Get directory information for the folder
		DirectoryInfo dirInfo = new DirectoryInfo(folder);

		//Add files to the tree with currentNode as the parent
		foreach (FileInfo fi in dirInfo.GetFiles("*.aspx")) {
			SiteMapNode fileNode = CreateFileNode(fi.FullName, parentNode, folderPath);
			if (fileNode != null)
				AddNode(fileNode, parentNode);
		}

		//Add nodes for each subfolder
		foreach (DirectoryInfo di in dirInfo.GetDirectories()) {
			SiteMapNode folderNode = CreateFolderNode(di.FullName, string.Concat(folderPath, di.Name, "/") + DefaultPageName);

			//Add the node
			if (folderNode != null) {
				AddNode(folderNode, parentNode);

				BuildSiteMapFromFileSystem(folderNode, string.Concat(folderPath, di.Name, "/"));
			}
		}
	}

	protected SiteMapNode CreateFileNode(string fullName, SiteMapNode parentNode, string folderPath)
	{
		//Don't add the default page (e.g., Default.aspx) if we are adding the default
		//page to the folder URLs...
		if (UseDefaultPageAsFolderUrl && string.Compare(Path.GetFileName(fullName), DefaultPageName, true) == 0) {
			return null;
		}

		//See if this is an excluded file
		if (IsExcludedFile(fullName)) {
			return null;
		}

		//Create the SiteMapNode
		return new SiteMapNode(this, fullName, folderPath + Path.GetFileName(fullName), GetFileTitle(fullName));
	}

	protected virtual string GetFileTitle(string fullName)
	{
		return Path.GetFileNameWithoutExtension(fullName).Replace("_", " ");
	}

	protected virtual string GetFolderTitle(string folderName, string defaultPageFullName)
	{
		return folderName.Replace("_", " ");
	}

	protected SiteMapNode CreateFolderNode(string fullName, string url)
	{
		//Do not add folders starting with App_ or the Bin folder
		if (string.Compare(Path.GetFileName(fullName), "Bin") == 0 || Path.GetFileName(fullName).StartsWith("App_", StringComparison.InvariantCultureIgnoreCase)) {
			return null;
		}

		//See if this is an excluded folder
		if (IsExcludedFolder(fullName)) {
			return null;
		}

		string fullUrlPath = HttpContext.Current.Server.MapPath(url);

		SiteMapNode folderNode = new SiteMapNode(this, fullName, string.Empty, GetFolderTitle(Path.GetFileName(fullName), fullUrlPath));

		//Only set the URL for the folder IF:
		//   (1) The UseDefaultPageAsFolderUrl setting is True (the default)
		//   (2) The Default Page exists (i.e., ~/Folder/Default.aspx exists)
		//   (3) The Default Page is not in the excluded file list
		if (UseDefaultPageAsFolderUrl && File.Exists(fullUrlPath) && !IsExcludedFile(fullUrlPath)) {
			folderNode.Url = url;
		}

		return folderNode;
	}

	private bool IsExcludedFile(string fullPath)
	{
		return StringDictionaryCheck(ExcludeFileList, fullPath);
	}

	private bool IsExcludedFolder(string fullPath)
	{
		return StringDictionaryCheck(ExcludeFolderList, fullPath);
	}

	private bool StringDictionaryCheck(StringDictionary dict, string fullPath)
	{
		return dict != null && dict.ContainsKey(fullPath.ToLower());
	}


	protected override System.Web.SiteMapNode GetRootNodeCore()
	{
		//Since BuildSiteMap() returns the root of the site map, just call BuildSiteMap()
		return BuildSiteMap();
	}

        }
    }