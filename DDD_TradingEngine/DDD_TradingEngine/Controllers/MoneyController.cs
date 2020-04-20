using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD_TradingEngine.MoneyDomain.Model;
using DDD_TradingEngine.UserDomain.Model;
using DDD_TradingEngine.UserDomain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD_TradingEngine.Controllers
{
    [Route("money")]
    [ApiController]
    public class MoneyController : Controller
    {
        private readonly IUserRepository _userRepository;

        public MoneyController (IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [Route("get-balance")]
        [HttpGet]
        public IActionResult GetBalance(int userId)
        {
            return Ok(_userRepository.GetUserBalance(userId));
        }

        [Route("add-money")]
        [HttpPost]
        public IActionResult AddMoney(int userId, [FromBody] AddMoney money)
        {
           var result = _userRepository.AddUserBalance(userId, money);

            if (result == true)
                return Ok(StatusCodes.Status200OK);
            else
                return BadRequest(StatusCodes.Status500InternalServerError);
        }

        [Route("exchange-money")]
        [HttpPost]
        public IActionResult ExchangeMoney(int userId, ExchangeMoney exhangeMoney)
        {
            var result = _userRepository.ExchangeMoney(exhangeMoney, userId);

            if (result == true)
                return Ok(StatusCodes.Status200OK);
            else
                return BadRequest(StatusCodes.Status500InternalServerError);
        }


        [Route("send-money")]
        [HttpPost]
        public IActionResult SendMoney(int userId, SendMoney sendMoney)
        {
            var result = _userRepository.SendMoney(sendMoney, userId);

            if (result == true)
                return Ok(StatusCodes.Status200OK);
            else
                return BadRequest(StatusCodes.Status500InternalServerError);
        }
    }
}