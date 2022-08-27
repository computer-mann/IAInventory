﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IAInventory.Controllers
{
    [Authorize(Roles = Pages.MainMenu.Shipment.RoleName)]
    public class ShipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}