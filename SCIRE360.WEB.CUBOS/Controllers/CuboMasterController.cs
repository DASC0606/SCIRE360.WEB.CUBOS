using C1.Web.Mvc.Olap;
using SCIRE360.WEB.CUBOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidad;
using Negocio;
using System.Data;
using DataAccess;
using SCIRE360.WEB.CUBOS.Util;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Http;

namespace SCIRE360.WEB.CUBOS.Controllers
{
    //CheckAuthorization]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CuboMasterController : ApiController
    {
        string defaultHREF = string.Empty;

        private static Dictionary<string, object[]> ChartSettings = new Dictionary<string, object[]>
        {
            {"ChartType", new object[] { PivotChartType.Column, PivotChartType.Area, PivotChartType.Bar, PivotChartType.Line, PivotChartType.Pie, PivotChartType.Scatter} }
        };


        // GET: Cube
        private readonly ClientSettingsModel OlapModells = new ClientSettingsModel
        {
            Settings = new Dictionary<string, object[]>
            {
                {"RowTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ColumnTotals", new object[] { ShowTotals.Subtotals, ShowTotals.None, ShowTotals.GrandTotals} },
                {"ShowZeros", new object[] { false, true } },
                { "AllowMerging", new object[] {
                  C1.Web.Mvc.Grid.AllowMerging.All,
                  C1.Web.Mvc.Grid.AllowMerging.AllHeaders,
                  C1.Web.Mvc.Grid.AllowMerging.Cells,
                  C1.Web.Mvc.Grid.AllowMerging.ColumnHeaders,
                  C1.Web.Mvc.Grid.AllowMerging.None,
                  C1.Web.Mvc.Grid.AllowMerging.RowHeaders
                } }
            }
        };

        // GET: CuboMaster
        /*public async Task<ActionResult> Index()
        {
            if (Session["UserData"] != null)
            {
                Session["opSel"] = 11;
                //Title = "Cubos Olap";
                DAO.RoutingExtent = "11";
                DataTable tabLogin = new DataTable();
                tabLogin = (DataTable)Session["UserData"];
                Session["USERID"] = tabLogin.Rows[0]["id_usuario"].ToString();
                Session["USER"] = tabLogin.Rows[0]["Nomb_usuario"].ToString();
                Session["RUC"] = tabLogin.Rows[0]["RUC"].ToString();
                Session["IDPERSONAL"] = tabLogin.Rows[0]["IdPersonal"].ToString();
                Session["DNI"] = tabLogin.Rows[0]["dni"].ToString();
                Session["EMPRESA"] = tabLogin.Rows[0]["Razn_social"].ToString();

                if (Session["DATEMP"] == null ||
                    Session["DATPLA"] == null ||
                    Session["DATEJE"] == null ||
                    Session["DATPER"] == null)
                {

                    Session["ConfigParam"] = 1;
                    defaultHREF = "setParametersinfo";
                    string strPage = defaultHREF;
                    return RedirectToAction("Index", strPage);
                }
                else
                {
                    Session["RUC"] = Session["RUCBK"].ToString();
                    Session["DATPER"] = Session["DATPERBK"].ToString();

                    Tools tools = new Tools();
                    DataTable OpcionMenu = new DataTable();
                    OpcionMenu = (DataTable)Session["OpcionMenu"];
                    if (tools.MasterConfig(OpcionMenu, "CuboMaster"))
                    {
                        Session["ConfigParam"] = 0;
                        //lblSetParameters.Text = Session["SELPER"] != null ? Session["SELPER"].ToString() : " Parametros no configurados ...";
                    }
                    else
                    {
                        Session["ConfigParam"] = 1;
                        defaultHREF = "setParametersinfo";
                        string strPage = defaultHREF;
                        return RedirectToAction("Index", strPage);
                    }
                }
            }
            else
            {
                Session["ConfigParam"] = 1;
                defaultHREF = "setParametersinfo";
                string strPage = defaultHREF;
                return RedirectToAction("Index", strPage);
            }

            return View();
        }*/


        [System.Web.Http.HttpGet]
        public int FillParametersCube(string ddl_compania, string ddl_planilla, string ddl_proceso, string ddl_cobertura, string ddl_ejercicio, string ddl_mes, string ddl_periodo, string cubo, string ddl_grafico)
        {
            bool DivInfoV = false;
            string DivInfoCss = "";
            string DivMessage = "";

            /*Session["ddl_compania"] = ddl_compania;
            Session["ddl_planilla"] = ddl_planilla;
            Session["ddl_proceso"] = ddl_proceso;
            Session["ddl_cobertura"] = ddl_cobertura;
            Session["ddl_ejercicio"] = ddl_ejercicio;
            Session["ddl_mes"] = ddl_mes;
            Session["ddl_periodo"] = ddl_periodo;

            Session["cubo"] = cubo;
            Session["ddl_grafico"] = ddl_grafico;*/



            if (ddl_periodo == "")
            {
                DivInfoV = false;
                DivInfoCss = "alert alert-danger";
                DivMessage = "Por favor seleccionar un periodo.";
            }
            else
            {
                DivInfoV = true;
                DivInfoCss = "";
                DivMessage = "";
            }

            var dataResult = new ParametersNotificacion_ebl

            {
                DivInfoV = DivInfoV,
                DivInfoCss = DivInfoCss,
                DivMessage = DivMessage
            };
            return 1;
        }
    }
}