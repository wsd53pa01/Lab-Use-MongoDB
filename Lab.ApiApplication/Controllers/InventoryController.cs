using System;
using System.Collections.Generic;
using Lab.Core.Entities;
using Lab.Core.Interface.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Lab.ApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class InventoryController
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IClientSessionHandle _clientSessionHandle;

        public InventoryController(IInventoryRepository inventoryRepository,
            IClientSessionHandle clientSessionHandle)
        {
            _inventoryRepository = inventoryRepository;
            _clientSessionHandle = clientSessionHandle;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            // _clientSessionHandle.StartTransaction();
            try
            {
                // _clientSessionHandle.CommitTransaction();
                List<Inventory> list = _inventoryRepository.Get();
                return new OkObjectResult(list);
            } catch (Exception e)
            {
                // _clientSessionHandle.AbortTransaction();
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}