using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
	public class Answer
	{
		public int Id { get; set; }

		public string Body { get; set; }
		public DateTime AnswerDate { get; set; }
		public int Upvotes { get; set; } = 0;
		public int Downvotes { get; set; } = 0;

		public int Question_Id { get; set; }
		[ForeignKey("Question_Id")]
		public virtual Question Question { get; set; }

		public string PosterId { get; set; }
		[ForeignKey("PosterId")]
		public virtual ApplicationUser Poster { get; set; }

		public ICollection<AnswerVote> Votes { get; set; }
	}
}