using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers.Master
{
    //[Authorize]
    public class MUsersController : Controller
    {
       


        public readonly IGenericService<Muser> _MUserService; 
        public MUsersController(IGenericService<Muser> MUserService)
        {
            _MUserService = MUserService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetDataIndex(cstmFilterDataTable param)
        {
            try
            {
                var result = await _MUserService.getDataPaging(param);
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

        [HttpGet("{paramIntRoleId}")]
        public async Task<IActionResult> GetById(int paramIntRoleId)
        {
            try
            {
                var MUser = await _MUserService.GetDataById(paramIntRoleId);

                if (MUser != null)
                {
                    return Ok(MUser);
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
        public async Task<IActionResult> Create(Muser paramMUserModel)
        {
            try
            {
                var isMUserCreated = await _MUserService.CreateData(paramMUserModel);

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
        public async Task<IActionResult> Update(Muser paramMUserModel)
        {
            try
            {
                if (paramMUserModel != null)
                {
                    var isMUserCreated = await _MUserService.UpdateData(paramMUserModel);
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

        [HttpDelete("{paramIntRoleId}")]
        public async Task<IActionResult> Delete(int paramIntRoleId)
        {
            try
            {
                var isMUserCreated = await _MUserService.DeleteData(paramIntRoleId);

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




    }
}
