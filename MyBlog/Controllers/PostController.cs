using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
	[Authorize]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;

		public PostController(AppDbContext  c)
		{
			_context=c;
		}
		/*private readonly List<Post> _listPosts;

		public PostController() 
        {
            _listPosts = GeneratePost();
        }*/
		/*public IActionResult Index()
        {
            Post post = new Post
            {
                Id = 1,
                Title = "Judulnya",
                Content = "Ini isi artikel",
                CreatedDate = DateTime.Now,
            };

            return View(post);
        }*/

		public IActionResult Index(int page = 1)
        {
			ViewBag.PrevPage = (page > 1) ? page - 1 : 1; // Mengatur halaman sebelumnya, jika halaman saat ini bukan halaman pertama
			ViewBag.NextPage = page + 1;
            int dataPerpage = 10;
            int skip = dataPerpage * page - dataPerpage;
            //List<Post> data = GeneratePost();
			List<Post> data = _context.Posts.ToList();
            List<Post> filtreddata = data
                //.Where(post =>post.Id<=10)
                .Skip(skip)
                .Take(dataPerpage)
                .OrderBy(post =>post.Id)//urutan dari yang terkecil
                //.OrderByDescending(post => post.Id)//urutan dari yang terbesar
                .ToList();
            return View(filtreddata);
        }


		/*public IActionResult Detail(string title)
        {
            ViewBag.Title = title;
            return View();
        }*/
		public IActionResult Detail(int id)
		{
			//Post data = _listPosts.Where(post => post.Id == id).FirstOrDefault();
			Post data = _context.Posts.Where(Post=>Post.Id == id).FirstOrDefault();
			return View(data);
		}

		public IActionResult Create() 
		{
			return View();
		}
		[HttpPost]
        public IActionResult Create([FromForm] Post data)
        {
            if (ModelState.IsValid)
            {
                // Validasi berhasil, set nilai Likes dan CreatedDate, dan tambahkan ke database
                data.Likes = 0;
                data.CreatedDate = DateTime.Now;
                _context.Posts.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Validasi gagal, kembalikan ke halaman Create dengan pesan kesalahan
                return View(data);
            }
        }


        public IActionResult Edit(int id)
		{
			var postData = _context.Posts.FirstOrDefault(x => x.Id == id);
			return View(postData);
		}

		[HttpPost]

		public IActionResult Edit([FromForm] Post data)
		{
			var dataFromDb = _context.Posts.FirstOrDefault(x => x.Id == data.Id);

			if(dataFromDb != null)
			{
				dataFromDb.Title = data.Title;
				dataFromDb.Content = data.Content;

				_context.Posts.Update(dataFromDb);
				_context.SaveChanges();
			}
			
			return RedirectToAction("Index");

		}

		public IActionResult Delete(int id)
		{
			var dataFromDb = _context.Posts.FirstOrDefault(x=> x.Id == id);

			if(dataFromDb != null)
			{
				_context.Posts.Remove(dataFromDb);
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}

        private List<Post> GeneratePost()
        {
			Random random = new Random();
			List<Post> posts = new List<Post>();
			int id = 1;

			for (int i = 0; i < 100; i++)
			{
				int likes = random.Next(1, 100); // Memberikan nilai like secara acak antara 1-100
				posts.Add(new Post
				{
					Id = id,
					Title = "Judul " + id,
					Content = "Ini isi artikel",
					CreatedDate = DateTime.Now,
					Likes = likes // Mengatur jumlah like untuk posting
				});
				id++;
			}
			return posts;
		}

		public IActionResult TopLikedPosts()
		{
			List<Post> data = GeneratePost();
			List<Post> topLikedPosts = data.OrderByDescending(post => post.Likes).Take(5).ToList();
			return View(topLikedPosts);
		}
	}
}