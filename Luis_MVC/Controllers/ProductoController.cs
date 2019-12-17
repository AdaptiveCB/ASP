using Luis_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


//tutorial: youtube.com/watch?v=1IFS33sPDhE

namespace Luis_MVC.Controllers
{
    public class ProductoController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-MMHB282; Initial Catalog = BCP_MVC; Integrated Security = True";
        //SqlConnection sqlCon = new SqlConnection("Server=DESKTOP-MMHB282;Database=BCP_WF;User Id=bcp;Password=123;");

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Producto", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductoModel());
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(ProductoModel productoModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Producto VALUES(@nombre,@precio,@cantidad)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@nombre", productoModel.nombre);
                sqlCmd.Parameters.AddWithValue("@precio", productoModel.precio);
                sqlCmd.Parameters.AddWithValue("@cantidad", productoModel.cantidad);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            ProductoModel productoModel = new ProductoModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Producto where id_producto = @idProducto";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@idProducto", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                productoModel.idProducto = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productoModel.nombre = dtblProduct.Rows[0][1].ToString();
                productoModel.precio = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productoModel.cantidad = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productoModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductoModel productoModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Producto SET nombre=@nombre,precio=@precio,cantidad=@cantidad WHERE id_producto=@idProducto";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@idProducto", productoModel.idProducto);
                sqlCmd.Parameters.AddWithValue("@nombre", productoModel.nombre);
                sqlCmd.Parameters.AddWithValue("@precio", productoModel.precio);
                sqlCmd.Parameters.AddWithValue("@cantidad", productoModel.cantidad);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Producto WHERE id_producto=@idProducto";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@idProducto", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}
