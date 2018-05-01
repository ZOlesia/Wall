using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using login_registration.Models;

namespace login_registration.Controllers
{
    public class WallController : Controller
    {

        private readonly DbConnector _dbConnector;

        public WallController(DbConnector connect)
        {
            _dbConnector = connect;
        }


        [HttpGet]
        [Route("wall")]
        public IActionResult WallDisplay()
        {
            int? UserId = HttpContext.Session.GetInt32("userId");
                if(UserId == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            ViewBag.allMessages = _dbConnector.Query("SELECT  messages.*,  DATE_FORMAT(messages.created_at, '%M %D %Y') as date, users.first_name as name FROM messages JOIN users ON messages.user_id = users.id ORDER BY created_at desc");
            ViewBag.allComments = _dbConnector.Query("SELECT comments.*, DATE_FORMAT(comments.created_at, '%M %D %Y') as date, users.first_name as name FROM comments JOIN users ON comments.user_id = users.id ORDER BY created_at desc");
            ViewBag.username = HttpContext.Session.GetString("username");
            return View("wall");}

        [HttpPost]
        [Route("post/message")]
        public IActionResult PostMessage(Message newMessage)
        {
            var user = HttpContext.Session.GetInt32("userId");
            if(user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            _dbConnector.Execute($"INSERT INTO messages (message, user_id) VALUES ('{newMessage.message}', {user});");

            return RedirectToAction("WallDisplay");
        }

        [HttpPost]
        [Route("post/comment")]
        public IActionResult PostComment(string comment, int msg_id)
        {
            var user = HttpContext.Session.GetInt32("userId");
            _dbConnector.Query($"INSERT INTO comments (comment, user_id, message_id) VALUES ('{comment}', {user}, '{msg_id}')");
            return RedirectToAction("WallDisplay");
        }
    }
}







