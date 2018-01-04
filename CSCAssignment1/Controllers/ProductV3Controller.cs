using CSCAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSCAssignment1.Controllers
{
    public class ProductV3Controller : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        //Version 3
        //[Authorize]
        [HttpGet]
        [Route("api/ProductV3/getAllProducts")]
        public IEnumerable<Product> GetAllProductsFromRepository()
        {

            return repository.GetAll();

        }

        [HttpGet]
        [Route("api/ProductV3/retrieveProducts/{id:int:min(1)}", Name = "getProductByIdv3")]

        public Product retrieveProductfromRepository(int id)
        {

            Product item = repository.Get(id);

            if (item == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            return item;
        }


        [HttpGet]
        [Route("api/ProductV3/getProductsByCategory", Name = "getProductByCategoryv3")]
        //http://localhost:9000/api/v3/products?category=
        //http://localhost:9000/api/v3/products?category=Groceries

        public IEnumerable<Product> GetProductsByCategory(string category)
        {

            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)); // ignores any cases.

        }

        [HttpPost]
        [Route("api/ProductV3/addProducts")]
        public HttpResponseMessage PostProduct(Product item)
        {
            if (ModelState.IsValid)
            {

                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

                // Generate a link to the new product and set the Location header in the response.
                string uri = Url.Link("getProductByIdv3", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return Request.CreateResponse(HttpStatusCode.OK, "Id " + item.Id + " " + item.Name + " is successfully added");

            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }

        [HttpPut]
        [Route("api/ProductV3/updateProducts/{id:int}")]
        public string PutProduct(int id, Product product)
        {
            product.Id = id;
            String message = "Product " + id + " has been updated!";

            if (!repository.Update(product))
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            else
            {

                return message; // returns a message if successfully updated.

            }
        }

        [HttpDelete]
        [Route("api/ProductV3/deleteProducts/{id:int}")]
        public string DeleteProduct(int id)
        {

            string message = "Product " + id + " has been deleted!";

            repository.Remove(id);
            return message; // returns a message if successfully deleted.

        }
    }
}
