using SafarObjects.TripClasses;
using SafarSDK.Core;

namespace SafarSDK.Models
{
    public class FellowModel : ModelBase
    {
        public string TripId { get; set; }
        public string UserId { get; set; }
        public FellowType FellowType { get; set; }
        public FellowStatus FellowStatus { get; set; }

        public FellowModel()
        {
            
        }

        
    }
}
