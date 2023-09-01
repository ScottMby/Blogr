using Blogr.Shared;

namespace Blogr.UnitTesting
{
    public class BlogUploadControllerTest
    {
        [Fact]
        public void Test1()
        {
            BlogUpload Bu = new BlogUpload();
            Bu.Title = "Title";
            Bu.Category = "Category";
            Bu.ContentPath = "//";
        }
    }
}