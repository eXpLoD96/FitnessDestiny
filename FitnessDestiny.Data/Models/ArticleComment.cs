﻿namespace FitnessDestiny.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class ArticleComment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
