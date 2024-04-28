namespace FinalProject.Models
{
	public class Topic
	{
		public int Id { get; set; }
		public string TopicName { get; set; }

		public ICollection<BlogTopic> BlogTopic { get; set; } = null!;
		public ICollection<Blog> Blog { get; set; } = null!;

	}
}
