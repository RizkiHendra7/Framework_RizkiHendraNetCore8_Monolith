using AutoMapper;
using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.DTO;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers.Master
{
    [Authorize]
    public class MRolesController : Controller
    {
        #region constructor
        private readonly IMapper _Mapper;
        private readonly IGenericService<Mrole> _MroleService;
        public MRolesController(IGenericService<Mrole> MUserService, IMapper Mapper)
        {
            _MroleService = MUserService;
            _Mapper = Mapper;
        }
        #endregion



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
                var result = await _MroleService.getWithDataTable(param);
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
                var result = await _MroleService.GetDataById(ParamUserId);
                if (result != null)
                {
                    DTOMRoles resultValue = _Mapper.Map<DTOMRoles>(result);
                    if (result.Introleid > 0)
                    {
                        resultValue.id = clsRijndael.Encrypt(result.Introleid.ToString()); 
                    }
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

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> GetAllData(string search = "")
        {
            try
            {
                List<DTOMRoles> resultValue = new List<DTOMRoles>();
                var result = await _MroleService.GetAllActiveData();
                if (result.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(search)) result = result.Where(x => x.Txtrolename.ToUpper().Contains(search.ToUpper())).ToList();

                    foreach (var item in result)
                    {
                        var tempData = _Mapper.Map<DTOMRoles>(item);
                        tempData.id = clsRijndael.Encrypt(item.Introleid.ToString()); 
                        resultValue.Add(tempData);
                    }

                }
                return Ok(resultValue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(DTOMRoles ParamDt)
        {
            try
            {
                Mrole paramData = _Mapper.Map<Mrole>(ParamDt);
                paramData.Introleid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.id));
                var isMUserCreated = await _MroleService.CreateData(paramData);

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

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(DTOMRoles ParamDt)
        {
            try
            {
                Mrole paramData = _Mapper.Map<Mrole>(ParamDt);
                paramData.Introleid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.id)); 
                var isMUserCreated = await _MroleService.UpdateData(paramData);

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
