﻿using System.ComponentModel.DataAnnotations;


namespace Account.DTO
{ 
    public class AccountInfoDTO
    {
        public int AccountId { get; set; }

        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [EmailAddress]
        [MaxLength(75)]
        public string Email { get; set; }

        public DateTime OpenDate { get; set; }

        public float Balance { get; set; }
    }
}
