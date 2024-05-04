using AutoMapper;
using FrameWorkRHP_Mono.Core.CommonFunction;
using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.DTO;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrameWorkRHP_Mono.Controllers.Master
{
    [Authorize]
    public class MRoleXMenusController : Controller
    {
        #region constructor
        private readonly IMapper _Mapper;
        private readonly IGenericService<MRoleXMenu> _MRoleXMenuService;
        public MRoleXMenusController(IGenericService<MRoleXMenu> MRoleXMenuService, IMapper Mapper)
        {
            _MRoleXMenuService = MRoleXMenuService;
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
                var result = await _MRoleXMenuService.getWithDataTable(param);
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
                var dtMenuXRole = await _MRoleXMenuService.GetDataById(ParamUserId);
                if (dtMenuXRole != null)
                {
                    DTOMroleXMenu resultValue = _Mapper.Map<DTOMroleXMenu>(dtMenuXRole);
                    if (dtMenuXRole.Intmenuid > 0)
                    {
                        resultValue.id = clsRijndael.Encrypt(dtMenuXRole.Intmrolexmenuid.ToString());
                        resultValue.menuId = clsRijndael.Encrypt(dtMenuXRole.Intmenuid.ToString());
                        resultValue.roleId = clsRijndael.Encrypt(dtMenuXRole.Introleid.ToString());
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
                List<DTOMroleXMenu> resultValue = new List<DTOMroleXMenu>();
                var ltMenuXRole = await _MRoleXMenuService.GetAllActiveData();
                if (ltMenuXRole.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(search))
                        ltMenuXRole = ltMenuXRole.Where(x => x.Intmenu.Txtmenuname.ToUpper().Contains(search.ToUpper()) || 
                                                             x.Introle.Txtrolename.ToUpper().Contains(search.ToUpper())
                                                             ).ToList();

                    foreach (var item in ltMenuXRole)
                    {
                        var tempData = _Mapper.Map<DTOMroleXMenu>(item); 
                        tempData.id = clsRijndael.Encrypt(item.Intmrolexmenuid.ToString());
                        tempData.menuId = clsRijndael.Encrypt(item.Intmenuid.ToString());
                        tempData.roleId = clsRijndael.Encrypt(item.Introleid.ToString());
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
        public async Task<IActionResult> Create(DTOMroleXMenu ParamDt)
        {
            try
            {
                MRoleXMenu paramData = _Mapper.Map<MRoleXMenu>(ParamDt);
                paramData.Intmrolexmenuid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.id));
                paramData.Introleid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.roleId));
                paramData.Intmenuid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.menuId));
                var isMUserCreated = await _MRoleXMenuService.CreateData(paramData);

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
        public async Task<IActionResult> Update(DTOMroleXMenu ParamDt)
        {
            try
            {
                MRoleXMenu paramData = _Mapper.Map<MRoleXMenu>(ParamDt);
                paramData.Intmrolexmenuid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.id));
                paramData.Introleid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.roleId));
                paramData.Intmenuid = Convert.ToInt32(clsRijndael.Decrypt(ParamDt.menuId));
                var isMUserCreated = await _MRoleXMenuService.UpdateData(paramData);

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
