﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspDotNetDemo.Models
{
    /// <summary>
    /// ログイン画面のViewModel。
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// ユーザID。
        /// </summary>
        [Required]
        [Display(Name = "ユーザID")]
        public string UserId { get; set; }

        /// <summary>
        /// パスワード。
        /// </summary>
        [Required]
        [Display(Name = "パスワード")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}