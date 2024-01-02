using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    public class RolController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RolController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Roles = roleManager.Roles.ToList();
            return View(Roles);
        }

        [HttpGet]
        public IActionResult Asignar(Guid IdRole, string Name)
        {
            ML.Result result = BL.IdentityUser.GetAll();
            ML.IdentityUser user = new ML.IdentityUser();

            if (result.Correct)
            {
                user.IdentityUsers = result.Objects;
            }

            user.Rol = new ML.Rol();
            user.Rol.Name = Name;
            user.Rol.RoleId = IdRole;

            return View(user);
        }

        [HttpPost]
        public IActionResult Asignar(ML.IdentityUser user)
        {
            ML.Result result = BL.IdentityUser.Asignar(user);

            if (result.Correct)
            {
                ViewBag.Message = "Todo ha salido bien";
            }
            else
            {
                ViewBag.Message = result.Message;
            }

            return PartialView("Modal");
        }
        [HttpPost]
        public async Task<IActionResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityRole rol)
        {

            if (ModelState.IsValid)
            {
                IdentityRole role = await roleManager.FindByIdAsync(rol.Id.ToString());

                if (role == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {

                    }
                }
                else
                {
                    role.Id = await roleManager.GetRoleIdAsync(rol);
                    role.Name = await roleManager.GetRoleNameAsync(rol);

                    IdentityResult result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                }
            }
            return View(rol);
        }

        [HttpGet]
        public IActionResult Form(Guid IdRole, string Name)
        {
            IdentityRole role = new IdentityRole();

            if (Name != null)
            {
                role.Name = Name;
                role.Id = IdRole.ToString();
                return View(role);
            }
            else
            {
                return View(role);
            }
        }

    }
}
