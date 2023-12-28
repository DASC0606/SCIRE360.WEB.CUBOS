using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace System.Linq.LinqExtensions
{


    


   // public class LinqExtensionsMethods
   //{
   

   
  //   /// <summary>
  ///// Hierarchy node class which contains a nested collection of hierarchy nodes
  ///// </summary>
  ///// <typeparam name="T">Entity</typeparam>
  //public class HierarchyNode<T> where T : class
  //{
  //  public T Entity { get; set; }
  //  public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
  //  public int Depth { get; set; }
  //}
 
  //public static class LinqExtensionMethods
  //{
  //  private static System.Collections.Generic.IEnumerable<HierarchyNode<TEntity>> CreateHierarchy<TEntity, TProperty>
  //    (IEnumerable<TEntity> allItems, TEntity parentItem, 
  //    Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, int depth) where TEntity : class
  //  { 
  //    IEnumerable<TEntity> childs;
 
  //    if (parentItem == null)
  //      childs = allItems.Where(i => parentIdProperty(i).Equals(default(TProperty)));
  //    else
  //      childs = allItems.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));
 
  //    if (childs.Count() > 0)
  //    {
  //      depth++;
 
  //      foreach (var item in childs)
  //        yield return new HierarchyNode<TEntity>() { Entity = item, ChildNodes = CreateHierarchy<TEntity, TProperty>
  //          (allItems, item, idProperty, parentIdProperty, depth), Depth = depth };
  //    }
  //  }
 
  //  /// <summary>
  //  /// LINQ IEnumerable AsHierachy() extension method
  //  /// </summary>
  //  /// <typeparam name="TEntity">Entity class</typeparam>
  //  /// <typeparam name="TProperty">Property of entity class</typeparam>
  //  /// <param name="allItems">Flat collection of entities</param>
  //  /// <param name="idProperty">Reference to Id/Key of entity</param>
  //  /// <param name="parentIdProperty">Reference to parent Id/Key</param>
  //  /// <returns>Hierarchical structure of entities</returns>
  //  public static System.Collections.Generic.IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>
  //    (this IEnumerable<TEntity> allItems, Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty)
  //    where TEntity : class
  //  {
  //    return CreateHierarchy(allItems, default(TEntity), idProperty, parentIdProperty, 0);
  //  }
  //}


    /// <summary>
    /// Hierarchy node class which contains a nested collection of hierarchy nodes
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class HierarchyNode<T> where T : class
    {
        public T Entity { get; set; }
        public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
        public int Depth { get; set; }
        public T Parent { get; set; }
    }

    /// <summary>
    /// AsHierarchy extension methods for LINQ to Objects IEnumerable
    /// </summary>
    public static class LinqToObjectsExtensionMethods
    {
        private static IEnumerable<HierarchyNode<TEntity>>
          CreateHierarchy<TEntity, TProperty>(
            IEnumerable<TEntity> allItems,
            TEntity parentItem,
            Func<TEntity, TProperty> idProperty,
            Func<TEntity, TProperty> parentIdProperty,
            object rootItemId,
            int maxDepth,
            int depth) where TEntity : class
        {
            IEnumerable<TEntity> childs;

            if (rootItemId != null)
            {
                childs = allItems.Where(i => idProperty(i).Equals(rootItemId));
            }
            else
            {
                if (parentItem == null)
                {
                    childs = allItems.Where(i => parentIdProperty(i).Equals(default(TProperty)));
                }
                else
                {
                    childs = allItems.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));
                }
            }

            if (childs.Count() > 0)
            {
                depth++;

                if ((depth <= maxDepth) || (maxDepth == 0))
                {
                    foreach (var item in childs)
                        yield return
                          new HierarchyNode<TEntity>()
                          {
                              Entity = item,
                              ChildNodes =
                                CreateHierarchy(allItems.AsEnumerable(), item, idProperty, parentIdProperty, null, maxDepth, depth),
                              Depth = depth,
                              Parent = parentItem
                          };
                }
            }
        }




        /// <summary>
        /// LINQ to Objects (IEnumerable) AsHierachy() extension method
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <typeparam name="TProperty">Property of entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="idProperty">Func delegete to Id/Key of entity</param>
        /// <param name="parentIdProperty">Func delegete to parent Id/Key</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>(
          this IEnumerable<TEntity> allItems,
          Func<TEntity, TProperty> idProperty,
          Func<TEntity, TProperty> parentIdProperty) where TEntity : class
        {
            return CreateHierarchy(allItems, default(TEntity), idProperty, parentIdProperty, null, 0, 0);
        }

        /// <summary>
        /// LINQ to Objects (IEnumerable) AsHierachy() extension method
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <typeparam name="TProperty">Property of entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="idProperty">Func delegete to Id/Key of entity</param>
        /// <param name="parentIdProperty">Func delegete to parent Id/Key</param>
        /// <param name="rootItemId">Value of root item Id/Key</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>(
          this IEnumerable<TEntity> allItems,
          Func<TEntity, TProperty> idProperty,
          Func<TEntity, TProperty> parentIdProperty,
          object rootItemId) where TEntity : class
        {
            return CreateHierarchy(allItems, default(TEntity), idProperty, parentIdProperty, rootItemId, 0, 0);
        }

        /// <summary>
        /// LINQ to Objects (IEnumerable) AsHierachy() extension method
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <typeparam name="TProperty">Property of entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="idProperty">Func delegete to Id/Key of entity</param>
        /// <param name="parentIdProperty">Func delegete to parent Id/Key</param>
        /// <param name="rootItemId">Value of root item Id/Key</param>
        /// <param name="maxDepth">Maximum depth of tree</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>(
          this IEnumerable<TEntity> allItems,
          Func<TEntity, TProperty> idProperty,
          Func<TEntity, TProperty> parentIdProperty,
          object rootItemId,
          int maxDepth) where TEntity : class
        {
            return CreateHierarchy(allItems, default(TEntity), idProperty, parentIdProperty, rootItemId, maxDepth, 0);
        }


        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
    Func<TSource, TKey> parentKeySelector,
    Func<TSource, TKey> childKeySelector,
    Func<TSource, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, resultSelector, Comparer<TKey>.Default);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector,
                (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, index, children));
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, IEnumerable<TResult>, TResult> resultSelector,
            IComparer<TKey> comparer)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector,
                (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, children), comparer);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, IEnumerable<TResult>, TResult> resultSelector,
            IComparer<TKey> comparer)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector,
                (TSource element, int depth, int index, IEnumerable<TResult> children) => resultSelector(element, index, children), comparer);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, int, IEnumerable<TResult>, TResult> resultSelector)
        {
            return RecursiveJoin(source, parentKeySelector, childKeySelector, resultSelector, Comparer<TKey>.Default);
        }

        public static IEnumerable<TResult> RecursiveJoin<TSource, TKey, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TKey> parentKeySelector,
            Func<TSource, TKey> childKeySelector,
            Func<TSource, int, int, IEnumerable<TResult>, TResult> resultSelector,
            IComparer<TKey> comparer)
        {
            // prevent source being enumerated more than once per RecursiveJoin call
            source = new LinkedList<TSource>(source);

            // fast binary search lookup
            SortedDictionary<TKey, TSource> parents = new SortedDictionary<TKey, TSource>(comparer);
            SortedDictionary<TKey, LinkedList<TSource>> children = new SortedDictionary<TKey, LinkedList<TSource>>(comparer);

            foreach (TSource element in source)
            {
                parents[parentKeySelector(element)] = element;

                LinkedList<TSource> list;

                TKey childKey = childKeySelector(element);

                if (!children.TryGetValue(childKey, out list))
                {
                    children[childKey] = list = new LinkedList<TSource>();
                }

                list.AddLast(element);
            }

            // initialize to null otherwise compiler complains at single line assignment
            Func<TSource, int, IEnumerable<TResult>> childSelector = null;

            childSelector = (TSource parent, int depth) =>
            {
                LinkedList<TSource> innerChildren = null;

                if (children.TryGetValue(parentKeySelector(parent), out innerChildren))
                {
                    return innerChildren.Select((child, index) => resultSelector(child, index, depth, childSelector(child, depth + 1)));
                }

                return Enumerable.Empty<TResult>();
            };

            return source.Where(element => !parents.ContainsKey(childKeySelector(element)))
                .Select((element, index) => resultSelector(element, index, 0, childSelector(element, 1)));
        }




    }


    ///// <summary>
    ///// AsHierarchy extension methods for LINQ to SQL IQueryable
    ///// </summary>
    //public static class LinqToSqlExtensionMethods
    //{
    //    private static IEnumerable<HierarchyNode<TEntity>>
    //      CreateHierarchy<TEntity>(IQueryable<TEntity> allItems,
    //        TEntity parentItem,
    //        string propertyNameId,
    //        string propertyNameParentId,
    //        object rootItemId,
    //        int maxDepth,
    //        int depth) where TEntity : class
    //    {
    //        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "e");
    //        Expression<Func<TEntity, bool>> predicate;

    //        if (rootItemId != null)
    //        {
    //            Expression left = Expression.Property(parameter, propertyNameId);
    //            left = Expression.Convert(left, rootItemId.GetType());
    //            Expression right = Expression.Constant(rootItemId);

    //            predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right), parameter);
    //        }
    //        else
    //        {
    //            if (parentItem == null)
    //            {
    //                predicate =
    //                  Expression.Lambda<Func<TEntity, bool>>(
    //                    Expression.Equal(Expression.Property(parameter, propertyNameParentId),
    //                                     Expression.Constant(null)), parameter);
    //            }
    //            else
    //            {
    //                Expression left = Expression.Property(parameter, propertyNameParentId);
    //                left = Expression.Convert(left, parentItem.GetType().GetProperty(propertyNameId).PropertyType);
    //                Expression right = Expression.Constant(parentItem.GetType().GetProperty(propertyNameId).GetValue(parentItem, null));

    //                predicate = Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(left, right), parameter);
    //            }
    //        }

    //        IEnumerable<TEntity> childs = allItems.Where(predicate).ToList();

    //        if (childs.Count() > 0)
    //        {
    //            depth++;

    //            if ((depth <= maxDepth) || (maxDepth == 0))
    //            {
    //                foreach (var item in childs)
    //                    yield return
    //                      new HierarchyNode<TEntity>()
    //                      {
    //                          Entity = item,
    //                          ChildNodes =
    //                            CreateHierarchy(allItems, item, propertyNameId, propertyNameParentId, null, maxDepth, depth),
    //                          Depth = depth,
    //                          Parent = parentItem
    //                      };
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// LINQ to SQL (IQueryable) AsHierachy() extension method
    //    /// </summary>
    //    /// <typeparam name="TEntity">Entity class</typeparam>
    //    /// <param name="allItems">Flat collection of entities</param>
    //    /// <param name="propertyNameId">String with property name of Id/Key</param>
    //    /// <param name="propertyNameParentId">String with property name of parent Id/Key</param>
    //    /// <returns>Hierarchical structure of entities</returns>
    //    public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(
    //      this IQueryable<TEntity> allItems,
    //      string propertyNameId,
    //      string propertyNameParentId) where TEntity : class
    //    {
    //        return CreateHierarchy(allItems, null, propertyNameId, propertyNameParentId, null, 0, 0);
    //    }

    //    /// <summary>
    //    /// LINQ to SQL (IQueryable) AsHierachy() extension method
    //    /// </summary>
    //    /// <typeparam name="TEntity">Entity class</typeparam>
    //    /// <param name="allItems">Flat collection of entities</param>
    //    /// <param name="propertyNameId">String with property name of Id/Key</param>
    //    /// <param name="propertyNameParentId">String with property name of parent Id/Key</param>
    //    /// <param name="rootItemId">Value of root item Id/Key</param>
    //    /// <returns>Hierarchical structure of entities</returns>
    //    public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(
    //      this IQueryable<TEntity> allItems,
    //      string propertyNameId,
    //      string propertyNameParentId,
    //      object rootItemId) where TEntity : class
    //    {
    //        return CreateHierarchy(allItems, null, propertyNameId, propertyNameParentId, rootItemId, 0, 0);
    //    }

    //    /// <summary>
    //    /// LINQ to SQL (IQueryable) AsHierachy() extension method
    //    /// </summary>
    //    /// <typeparam name="TEntity">Entity class</typeparam>
    //    /// <param name="allItems">Flat collection of entities</param>
    //    /// <param name="propertyNameId">String with property name of Id/Key</param>
    //    /// <param name="propertyNameParentId">String with property name of parent Id/Key</param>
    //    /// <param name="rootItemId">Value of root item Id/Key</param>
    //    /// <param name="maxDepth">Maximum depth of tree</param>
    //    /// <returns>Hierarchical structure of entities</returns>
    //    public static IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity>(
    //      this IQueryable<TEntity> allItems,
    //      string propertyNameId,
    //      string propertyNameParentId,
    //      object rootItemId,
    //      int maxDepth) where TEntity : class
    //    {
    //        return CreateHierarchy(allItems, null, propertyNameId, propertyNameParentId, rootItemId, maxDepth, 0);
    //    }
    //}

    //}

    


 
}