using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Data;
using System.Data.OleDb;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        const string dataSource = "";
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataTable table;

        OleDbConnection connStr = new($@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {dataSource}");
        private OleDbDataAdapter adapter;

        public DataSet FillingFromTableGoodsArea()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Область товаров]", connStr);
            adapter.Fill(dataSet, "[Область товаров]");

            return dataSet;
        }

        public DataSet FillingFromTableGoods()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Товары", connStr);
            adapter.Fill(dataSet, "Товары");

            return dataSet;
        }

        public DataSet FillingFromTableSales()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Продажи", connStr);
            adapter.Fill(dataSet, "Продажи");

            return dataSet;
        }

        public DataSet FillingFromTableBuyer()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Покупатели", connStr);
            adapter.Fill(dataSet, "Покупатели");

            return dataSet;
        }

        public DataSet FillingFromTableEmployees()
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Сотрудники", connStr);
            adapter.Fill(dataSet, "Сотрудники");

            return dataSet;
        }

        public ActionResult Index()
        {
            dataSet = FillingFromTableGoodsArea();
            DataTable table = dataSet.Tables[0];

            ViewBag.Dis = table.AsEnumerable();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            dataSet = FillingFromTableGoods();
            DataTable table = dataSet.Tables[0];

            var st = from s in table.AsEnumerable()
                     where (string)s["Область товаров"] == name
                     select s;

            ViewBag.Dis = st.AsEnumerable();
            return View("Goods");
        }

        public ActionResult Info(int id)
        {
            dataSet = FillingFromTableBuyer();
            DataTable table = dataSet.Tables[0];

            adapter = new OleDbDataAdapter("SELECT * FROM Сотрудники", connStr);
            adapter.Fill(dataSet1, "Сотрудники");
            DataTable table1 = dataSet1.Tables[0];

            adapter = new OleDbDataAdapter("SELECT * FROM Покупатели", connStr);
            adapter.Fill(dataSet2, "Покупатели");
            DataTable table2 = dataSet2.Tables[0];

            var st = from s in table.AsEnumerable()
                     where ((int)s["ID продукта"] == id)
                     select s;
            ViewBag.Dis = st.AsEnumerable();
            return View();
        }
    }
}