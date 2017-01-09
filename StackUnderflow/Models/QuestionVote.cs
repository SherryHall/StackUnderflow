using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StackUnderflow.Models
{
	public class QuestionVote
	{
		public int Id { get; set; }
		public int VoteUpOrDown { get; set; }

		public string Voter_Id { get; set; }
		[ForeignKey("Voter_Id")]
		public virtual ApplicationUser Voter { get; set; }

		public int Question_Id { get; set; }
		[ForeignKey("Question_Id")]
		public virtual Question Question { get; set; }
	}
}