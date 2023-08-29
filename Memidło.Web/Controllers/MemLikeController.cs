using Memidło.Web.Models.View;
using Memidło.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Memidło.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemLikeController : Controller
    {
        private readonly ILikeRepository likeRepository;
        public MemLikeController(ILikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        [HttpGet]
        [Route("{memId:int}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] int memId)
        {
            var totalLikes = await likeRepository.GetTotalLikesForMem(memId);

            return Ok(totalLikes);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] LikeVM likeVM)
        {
            await likeRepository.AddLikeForMem(likeVM.MemId, likeVM.UserId);

            return Ok();
        }
    }
}
