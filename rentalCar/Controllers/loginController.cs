using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using carBuisnnesLogic;
using System.Security.Claims;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rentalCar.Controllers
{
    [Route("api/allCars/login")]
    [ApiController]
    public class loginController : ControllerBase
    {

        //step 4 jwt start --dependency injection Configuration to controller
        IConfiguration config;

        public loginController(IConfiguration conf)
        {
            config = conf;
        }
        //step 4 jwt end

        // GET: api/<loginController>
        [HttpGet]
        public string Get(string ab = "")
        {
            //return new string[] { "value1", "value2" };
            return ab;
        }

        // GET api/<loginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        //step 5 jwt start 

        // POST api/<loginController>
        [HttpPost]
        public IActionResult PostLogin([FromBody]  UserDetail userEnter)
        {

            IActionResult response1 = Unauthorized();

            UserDetail userAcc =  new dbMange().chekUserPassLogin(userEnter) ;

            if (userAcc.UserName == userEnter.UserName && userAcc.Password == userEnter.Password)
            {
                var securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["jwt1:SecretKey1"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[] {
                  new Claim(JwtRegisteredClaimNames.Sub,"login"),
                  new Claim(ClaimTypes.Name,userAcc.UserName),
                  new Claim(ClaimTypes.Role,userAcc.Role),   //get the role from userX from db
                  new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(issuer: config["jwt1:Issuer1"],
                    audience: config["jwt1:Audience1"], claims: claims,
                    expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
                string tok = new JwtSecurityTokenHandler().WriteToken(token);
                response1 = Ok(new { token1 = tok, userDetails1 = userAcc });
            }

            return response1;
        }

        //step 5 jwt end 

        // PUT api/<loginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost("[action]")] // this api allow to insert a user,
        public IActionResult PostNewuserdetails([FromBody] UserDetail newUser)
        {
            if (newUser.UserName != null || newUser.UserName  != null)
            {
                return Ok(new dbMange().insertNewUserDetails(

                                                          newUser.FullName,
                                                          newUser.UserName,
                                                          newUser.Bdate.ToString(),
                                                          newUser.Gender,
                                                          newUser.Email,
                                                          newUser.Password,
                                                          newUser.Picture
                                                           ));
            }
            return NotFound();
        }


        // DELETE api/<loginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
