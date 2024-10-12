using Beautiful_Villa_API.Models.Dto;

namespace Beautiful_Villa_API.Controllers.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>(){
        new VillaDTO() {Id=1, Name="Pool View",Sqft=200,Occupancy=5 },
        new VillaDTO() {Id=2, Name="Beach View",Sqft=100,Occupancy=3}
    };
    }
}