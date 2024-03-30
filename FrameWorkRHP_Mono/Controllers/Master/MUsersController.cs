using AutoMapper;
using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.DTO;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Core.Models.ViewModels;
using FrameWorkRHP_Mono.Infrastructure.Repository;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FrameWorkRHP_Mono.Controllers.Master
{

    [Authorize]
    //[CustomAuthorize]
    public class MUsersController : Controller
    { 
        private readonly IMapper _Mapper;
        private readonly IGenericService<Muser> _MUserService;  
        public MUsersController(IGenericService<Muser> MUserService, IMapper Mapper)
        {
            _MUserService = MUserService;
            _Mapper = Mapper; 
        }

         
        public IActionResult Index()
        {
            return View();
        }
         
        public IActionResult Details(string id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetDataIndex(cstmFilterDataTable param)
        {
            try
            {
                //var result = new cstmResultModelDataTable();
                var result = await _MUserService.getWithDataTable(param);
                return Json(result);
            }
            catch (Exception ex)
            { 
                cstmResultModelDataTable result = new cstmResultModelDataTable();
                result.errorMessage = ex.Message;
                result.recordsTotal = 0;
                result.recordsFiltered = 0;
                return Json(result);
            }


        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> GetById(int ParamUserId)
        {
            try
            {
                var MUser = await _MUserService.GetDataById(ParamUserId);
                if (MUser != null)
                {
                    DTOMusers resultValue = _Mapper.Map<DTOMusers>(MUser);
                    return Ok(resultValue);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(DTOMusers ParamMUserModel)
        {
            try
            {
                Muser paramData = _Mapper.Map<Muser>(ParamMUserModel); 
                var isMUserCreated = await _MUserService.CreateData(paramData);

                if (isMUserCreated)
                {
                    return Ok(isMUserCreated);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        //[AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(DTOMusers ParamMUserModel)
        {
            try
            {
                if (ParamMUserModel != null)
                {
                    Muser paramData = _Mapper.Map<Muser>(ParamMUserModel);
                    var isMUserCreated = await _MUserService.UpdateData(paramData);
                    if (isMUserCreated)
                    {
                        return Ok(isMUserCreated);
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
