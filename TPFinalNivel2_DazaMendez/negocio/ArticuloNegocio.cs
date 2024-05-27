using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using accesoDatos;
using System.Text.RegularExpressions;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria,ImagenUrl, Precio, A.Id from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca And C.Id = A.IdCategoria");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @img, @precio)");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@img", nuevo.UrlImagen);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally 
            { 
                datos.cerrarConexion(); 
            }
        }
        public void modificar(Articulo modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update ARTICULOS set Codigo = @cod, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @idCate, ImagenUrl  = @img, Precio = @precio where Id = @id");
                datos.setearParametro("@cod", modificado.Codigo);
                datos.setearParametro("@nombre", modificado.Nombre);
                datos.setearParametro("@desc", modificado.Descripcion);
                datos.setearParametro("@idMarca", modificado.Marca.Id);
                datos.setearParametro("@idCate", modificado.Categoria.Id);
                datos.setearParametro("@img", modificado.UrlImagen);
                datos.setearParametro("@precio", modificado.Precio);
                datos.setearParametro("@id", modificado.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete from ARTICULOS where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro, string tipo, string subtipo)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, A.IdMarca, A.IdCategoria,ImagenUrl, Precio, A.Id from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca And C.Id = A.IdCategoria And ";
                if(tipo == "Marca")
                {
                    switch (subtipo)
                    {
                        case "Samsung":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 1 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 1 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 1 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 1 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 1 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 1 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 1 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 1 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 1 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdMarca = 1 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdMarca = 1 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdMarca = 1 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Apple":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 2 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 2 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 2 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 2 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 2 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 2 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 2 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 2 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 2 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdMarca = 2 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdMarca = 2 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdMarca = 2 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Sony":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 3 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 3 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 3 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 3 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 3 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 3 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 3 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 3 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 3 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdMarca = 3 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdMarca = 3 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdMarca = 3 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Huawei":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 4 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 4 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 4 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 4 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 4 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 4 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 4 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 4 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 4 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdMarca = 4 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdMarca = 4 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdMarca = 4 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Motorola":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 5 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 5 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 5 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 5 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 5 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 5 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdMarca = 5 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdMarca = 5 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdMarca = 5 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdMarca = 5 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdMarca = 5 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdMarca = 5 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (subtipo)
                    {
                        case "Celulares":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 1 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 1 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 1 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 1 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 1 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 1 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 1 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 1 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 1 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdCategoria = 1 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdCategoria = 1 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdCategoria = 1 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Televisores":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 2 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 2 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 2 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 2 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 2 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 2 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 2 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 2 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 2 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdCategoria = 2 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdCategoria = 2 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdCategoria = 2 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Media":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 3 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 3 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 3 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 3 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 3 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 3 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 3 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 3 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 3 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdCategoria = 3 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdCategoria = 3 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdCategoria = 3 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "Audio":
                            switch (campo)
                            {
                                case "Código":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 4 And Codigo like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 4 And Codigo like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 4 And Codigo like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Nombre":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 4 And Nombre like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 4 And Nombre like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 4 And Nombre like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Descripción":
                                    switch (criterio)
                                    {
                                        case "Comienza con":
                                            consulta += "IdCategoria = 4 And A.Descripcion like '" + filtro + "%'";
                                            break;
                                        case "Termina con":
                                            consulta += "IdCategoria = 4 And A.Descripcion like '%" + filtro + "'";
                                            break;
                                        default:
                                            consulta += "IdCategoria = 4 And A.Descripcion like '%" + filtro + "%'";
                                            break;
                                    }
                                    break;
                                case "Precio":
                                    switch (criterio)
                                    {
                                        case "Mayor a":
                                            consulta += "IdCategoria = 4 And Precio > " + filtro;
                                            break;
                                        case "Menor a":
                                            consulta += "IdCategoria = 4 And Precio < " + filtro;
                                            break;
                                        default:
                                            consulta += "IdCategoria = 4 And Precio = " + filtro;
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read()) 
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
