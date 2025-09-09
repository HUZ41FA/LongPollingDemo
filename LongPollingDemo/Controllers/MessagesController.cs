using LongPollingDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LongPollingDemo.Controllers
{
    [ApiController]
    public class MessagesController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MessagesController> _logger;
        public MessagesController(ApplicationDbContext context, ILogger<MessagesController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [HttpGet("api/messages/longpoll")]
        public async Task<IActionResult> GetMessages(DateTime? lastTimeStamp, CancellationToken cancellationToken)
        {
            var timeout = TimeSpan.FromSeconds(30);
            var startTime = DateTime.UtcNow;

            while (DateTime.Now - startTime > timeout)
            {
                var newMessages = await _dbContext.Messages
                    .Where(x => x.DeletedDate == null && (lastTimeStamp == null || x.CreatedDate > lastTimeStamp))
                    .OrderBy(x => x.CreatedDate)
                    .ToListAsync();

                if(newMessages.Any())
                {
                    newMessages.ForEach(x => x.ReadCounter++);
                    _dbContext.SaveChanges();
                    return Ok(newMessages);
                }
            }

            return NoContent();
        }
    }
}
