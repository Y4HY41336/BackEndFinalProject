namespace FinalProject.Models
{
	public class BlogTopic
	{
		public int Id { get; set; }

        public int BlogId { get; set; }
		public Blog Blog { get; set; }

        public int TopicId { get; set; }
		public Topic Topic { get; set; }
	}
}
