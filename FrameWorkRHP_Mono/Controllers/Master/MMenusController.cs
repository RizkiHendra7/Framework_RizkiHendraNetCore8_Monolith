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
    public class MMenusController : Controller
    {
        #region constructor
        private readonly IMapper _Mapper;
        public readonly IGenericService<MMenu> _MMenuService;
        public MMenusController(IGenericService<MMenu> MUserService, IMapper Mapper)
        {
            _MMenuService = MUserService;
            _Mapper = Mapper;
        }
        #endregion


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetDataIndex(cstmFilterDataTable param)
        {
            try
            {
                var result = await _MMenuService.getWithDataTable(param);
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
                var dtMenu = await _MMenuService.GetDataById(ParamUserId);
                if (dtMenu != null)
                {
                    DTOMMenus resultValue = _Mapper.Map<DTOMMenus>(dtMenu);
                    if(dtMenu.Intmenuid > 0)
                    { 
                        resultValue.id = clsRijndael.Encrypt(dtMenu.Intmenuid.ToString());
                        resultValue.idParent = clsRijndael.Encrypt(dtMenu.Intparentmenuid.ToString());
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
               List<DTOMMenus> resultValue = new List<DTOMMenus>();
                var ltMenu = await _MMenuService.GetAllActiveData();
                if (ltMenu.Count() > 0)
                {
                    if (!string.IsNullOrEmpty(search)) ltMenu = ltMenu.Where(x => x.Txtmenuname.ToUpper().Contains( search.ToUpper())).ToList();

                    foreach (var item in ltMenu)
                    {
                        var tempData = _Mapper.Map<DTOMMenus>(item);
                        tempData.id = clsRijndael.Encrypt(item.Intmenuid.ToString());
                        tempData.idParent = clsRijndael.Encrypt(item.Intparentmenuid.ToString());
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
        public async Task<IActionResult> Create(DTOMMenus ParamDt)
        {
            try
            {
                MMenu paramData = _Mapper.Map<MMenu>(ParamDt);
                var isMUserCreated = await _MMenuService.CreateData(paramData);

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
        public async Task<IActionResult> Update(DTOMMenus ParamDt)
        {
            try
            {
                MMenu paramData = _Mapper.Map<MMenu>(ParamDt);
                var isMUserCreated = await _MMenuService.UpdateData(paramData);

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
