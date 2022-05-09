using System;
using System.Collections.Generic;

namespace FitApp.UserPrivateDietDetailRepository.Model
{
    public class UserPrivateDietDetail : Entity
    {
        public Guid Id { get; set; }
        public List<Diet> Diets { get; set; }
    }

    public class Diet
    {
        public List<string> Goal { get; set; }
        public List<string> LikedFoods { get; set; }
        public List<string> DislikedFoods { get; set; }
        public string AdditionalInformation { get; set; }
        public string Disease { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}