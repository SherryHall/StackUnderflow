using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
	public class Question
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Title { get; set; }

		public string Body { get; set; }
		public DateTime QuestionDate { get; set; }
		public int Upvotes { get; set; } = 0;
		public int Downvotes { get; set; } = 0;
		public int ViewCount { get; set; } = 0;

		public string PosterId { get; set; }
		[ForeignKey("PosterId")]
		public virtual ApplicationUser Poster { get; set; }

		public ICollection<Answer> Answers { get; set; }
		public ICollection<QuestionVote> Votes { get; set; }
	}
}