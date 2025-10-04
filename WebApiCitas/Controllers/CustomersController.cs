using System.Web.Http;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// Provides endpoints for managing customer data.
    /// </summary>
    /// <remarks>This controller includes actions to retrieve customer information, such as fetching a
    /// specific customer by ID or retrieving a list of all customers. The data returned by these actions is currently
    /// predefined for demonstration purposes.</remarks>
    public class CustomersController : ApiController
    {
        /// <summary>
        /// Metodo para obtener un cliente por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var customerFake = "customer-fake";
            return Ok(customerFake);
        }

        /// <summary>
        /// Retrieves a list of all customers.
        /// </summary>
        /// <remarks>This method returns a predefined list of customer names for demonstration
        /// purposes.</remarks>
        /// <returns>An <see cref="IHttpActionResult"/> containing an array of strings, where each string represents a customer.</returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var customersFake = new string[] { "customer-1", "customer-2", "customer-3" };
            return Ok(customersFake);
        }
    }
}
