﻿using FrameWorkRHP_Mono.Core.Models.Custom;
using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;


namespace FrameWorkRHP_Mono.Services.ServicesImplement
{
    public class MMenuServices : IGenericService<MMenu>, IGeneratedMenu
    {
        public IUnitOfWork _unitOfWork;
        public MMenuServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateData(MMenu ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MMenus.InsertAsync(ParamModels);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteData(int ParamIntId)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MMenus.DeleteAsync(ParamIntId);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public Task<IEnumerable<MMenu>> GetAllActiveData()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MMenu>> GetAllData()
        {
            return await _unitOfWork.MMenus.GetAllAsync();
        }

        public async Task<MMenu> GetDataById(int ParamIntId)
        {
            var MmenuData = await _unitOfWork.MMenus.GetByIdAsync(Convert.ToInt32(ParamIntId));
            MmenuData = MmenuData == null ? new     MMenu() : MmenuData;
            return MmenuData;
        }

        public async Task<bool> UpdateData(MMenu ParamModels)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                await _unitOfWork.MMenus.UpdateAsync(ParamModels);
                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
         
        public async Task<string> generatedDynamicMenu(int paramRoleId)
        {
            try
            {
                var result = string.Empty;
                var roleXMenu = await _unitOfWork.MRoleXMenus.GetAllAsync();
                var allMenu = await _unitOfWork.MMenus.GetAllAsync();
                var idMenuRelated = roleXMenu.Where(x => x.Introleid == paramRoleId).Select(x => x.Intmenuid).ToList();
                allMenu = allMenu.Where(x => idMenuRelated.Contains(x.Intmenuid)).OrderBy(x => x.Intparentmenuid).ToList();
                var dtParentMenu = allMenu.Where(x => x.Intparentmenuid == 0).ToList();

                foreach (var menu in dtParentMenu)
                {
                    result += " <li class=\"nav-item\">";
                    result += "<a href=\"" + menu.Txturl + "\" " +
                                "class=\"nav-link\">\r\n <i class=\"" + menu.Txtmenuicon + "\"></i>\r\n " +
                                    "<p>\r\n " + menu.Txtmenudisplay + "\r\n";

                    if (allMenu.Where(x => x.Intparentmenuid == menu.Intmenuid).Count() > 0)
                    {
                        //KALAU SUDAH MASUK DISINI JELAS ADA CHILD NYA DI MENU TSB.
                        result += " <i class=\"right fas fa-angle-left\"></i>\r\n</p>\r\n</a>";
                        foreach (var childMenu in allMenu.Where(x => x.Intparentmenuid == menu.Intmenuid).ToList())
                        {
                            result = await generatedDynamicChildMenu(result, allMenu, childMenu);
                            result += " </ul>";
                        }
                        result += "\r\n </li>";
                    }
                    else
                    {
                        result += "</p>\r\n </a>\r\n </li>";
                    } 
                } 
                return result;
            }
            catch (Exception ex)
            { 
                throw;
            } 
        }

         public async Task<string> generatedDynamicChildMenu(string paramHtmlMenu, IEnumerable<MMenu> paramAllMenu, MMenu paramModelParentMenu)
        {
            paramHtmlMenu += " <ul class=\"nav nav-treeview\">";
            paramHtmlMenu += "<li class=\"nav-item\">\r\n <a href=\"" + paramModelParentMenu.Txturl + "\" class=\"nav-link\">\r\n <i class=\"far fa-circle nav-icon\"></i>\r\n <p>" + paramModelParentMenu.Txtmenudisplay ;
            if (paramAllMenu.Where(x => x.Intparentmenuid == paramModelParentMenu.Intmenuid).Count() > 0)
            {
                //KALAU SUDAH MASUK DISINI JELAS ADA CHILD NYA DI MENU TSB.
                paramHtmlMenu += " <i class=\"right fas fa-angle-left\"></i>\r\n</p>\r\n</a>";
                foreach (var childMenu in paramAllMenu.Where(x => x.Intparentmenuid == paramModelParentMenu.Intmenuid).ToList())
                {
                    paramHtmlMenu = await generatedDynamicChildMenu(paramHtmlMenu, paramAllMenu, childMenu);
                    paramHtmlMenu += " </ul>\r\n </li>";
                }
            }
            else
            {
                paramHtmlMenu += "</p>\r\n </a>\r\n </li>";
            } 

            return paramHtmlMenu;
        }

        public Task<cstmResultModelDataTable> getWithDataTable(cstmFilterDataTable paramModel)
        {
            throw new NotImplementedException();
        }
    }
}
