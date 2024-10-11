using Beautiful_Villa_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beautiful_Villa_API.Controllers
{
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public IEnumerable<Villa> GetVillas() 
        {
            return new List<Villa>()
            {
                new Villa() {Id=1, Name="Pool View" },
                new Villa() {Id=2, Name="Beach View" }
            };
        }   
    }
}
