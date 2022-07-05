using System;
using System.Collections.Generic;

namespace FitApp.UserPrivateDietRepository.Model
{
    public class UserPrivateDiet : Entity
    {
        public Guid Id { get; set; }
        public List<string> Goal { get; set; }
        public List<string> LikedFoods { get; set; }
        public List<string> DislikedFoods { get; set; }
        public Dictionary<int, string> WeeksMenuNameDictionary { get; set; }
        public string AdditionalInformation { get; set; }
        public string Disease { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}