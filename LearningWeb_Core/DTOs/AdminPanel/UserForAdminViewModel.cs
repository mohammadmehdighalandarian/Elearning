﻿using DataLayer.Entities.User;

namespace LearningWeb_Core.DTOs.AdminPanel
{
    public class UserForAdminViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
