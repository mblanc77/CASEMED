using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using SgpaNew.Server.Data;

namespace SgpaNew.Server
{
    public partial class SgpaService
    {
        SgpaContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly SgpaContext context;
        private readonly NavigationManager navigationManager;

        public SgpaService(SgpaContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportAdPreJubsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAdPreJubsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAdPreJubsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJub> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJub>> GetAdPreJubs(Query query = null)
        {
            var items = Context.AdPreJubs.AsQueryable();

            items = items.Include(i => i.Afiliado);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAdPreJubsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAdPreJubGet(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnGetAdPreJubByCi(ref IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJub> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> GetAdPreJubByCi(int ci)
        {
            var items = Context.AdPreJubs
                              .AsNoTracking()
                              .Where(i => i.CI == ci);

            items = items.Include(i => i.Afiliado);
 
            OnGetAdPreJubByCi(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAdPreJubGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAdPreJubCreated(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnAfterAdPreJubCreated(SgpaNew.Server.Models.Sgpa.AdPreJub item);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> CreateAdPreJub(SgpaNew.Server.Models.Sgpa.AdPreJub adprejub)
        {
            OnAdPreJubCreated(adprejub);

            var existingItem = Context.AdPreJubs
                              .Where(i => i.CI == adprejub.CI)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AdPreJubs.Add(adprejub);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(adprejub).State = EntityState.Detached;
                throw;
            }

            OnAfterAdPreJubCreated(adprejub);

            return adprejub;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> CancelAdPreJubChanges(SgpaNew.Server.Models.Sgpa.AdPreJub item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAdPreJubUpdated(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnAfterAdPreJubUpdated(SgpaNew.Server.Models.Sgpa.AdPreJub item);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> UpdateAdPreJub(int ci, SgpaNew.Server.Models.Sgpa.AdPreJub adprejub)
        {
            OnAdPreJubUpdated(adprejub);

            var itemToUpdate = Context.AdPreJubs
                              .Where(i => i.CI == adprejub.CI)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(adprejub);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAdPreJubUpdated(adprejub);

            return adprejub;
        }

        partial void OnAdPreJubDeleted(SgpaNew.Server.Models.Sgpa.AdPreJub item);
        partial void OnAfterAdPreJubDeleted(SgpaNew.Server.Models.Sgpa.AdPreJub item);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJub> DeleteAdPreJub(int ci)
        {
            var itemToDelete = Context.AdPreJubs
                              .Where(i => i.CI == ci)
                              .Include(i => i.AdPreJubPagos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAdPreJubDeleted(itemToDelete);


            Context.AdPreJubs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAdPreJubDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAdPreJubPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAdPreJubPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/adprejubpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/adprejubpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAdPreJubPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJubPago> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJubPago>> GetAdPreJubPagos(Query query = null)
        {
            var items = Context.AdPreJubPagos.AsQueryable();

            items = items.Include(i => i.AdPreJub);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAdPreJubPagosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAdPreJubPagoGet(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnGetAdPreJubPagoByAdPreJubPagoId(ref IQueryable<SgpaNew.Server.Models.Sgpa.AdPreJubPago> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> GetAdPreJubPagoByAdPreJubPagoId(int adprejubpagoid)
        {
            var items = Context.AdPreJubPagos
                              .AsNoTracking()
                              .Where(i => i.AdPreJubPagoId == adprejubpagoid);

            items = items.Include(i => i.AdPreJub);
 
            OnGetAdPreJubPagoByAdPreJubPagoId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAdPreJubPagoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAdPreJubPagoCreated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnAfterAdPreJubPagoCreated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> CreateAdPreJubPago(SgpaNew.Server.Models.Sgpa.AdPreJubPago adprejubpago)
        {
            OnAdPreJubPagoCreated(adprejubpago);

            var existingItem = Context.AdPreJubPagos
                              .Where(i => i.AdPreJubPagoId == adprejubpago.AdPreJubPagoId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AdPreJubPagos.Add(adprejubpago);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(adprejubpago).State = EntityState.Detached;
                throw;
            }

            OnAfterAdPreJubPagoCreated(adprejubpago);

            return adprejubpago;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> CancelAdPreJubPagoChanges(SgpaNew.Server.Models.Sgpa.AdPreJubPago item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAdPreJubPagoUpdated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnAfterAdPreJubPagoUpdated(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> UpdateAdPreJubPago(int adprejubpagoid, SgpaNew.Server.Models.Sgpa.AdPreJubPago adprejubpago)
        {
            OnAdPreJubPagoUpdated(adprejubpago);

            var itemToUpdate = Context.AdPreJubPagos
                              .Where(i => i.AdPreJubPagoId == adprejubpago.AdPreJubPagoId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(adprejubpago);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAdPreJubPagoUpdated(adprejubpago);

            return adprejubpago;
        }

        partial void OnAdPreJubPagoDeleted(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);
        partial void OnAfterAdPreJubPagoDeleted(SgpaNew.Server.Models.Sgpa.AdPreJubPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.AdPreJubPago> DeleteAdPreJubPago(int adprejubpagoid)
        {
            var itemToDelete = Context.AdPreJubPagos
                              .Where(i => i.AdPreJubPagoId == adprejubpagoid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAdPreJubPagoDeleted(itemToDelete);


            Context.AdPreJubPagos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAdPreJubPagoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAfeccionGruposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciongrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciongrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAfeccionGruposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciongrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciongrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAfeccionGruposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo>> GetAfeccionGrupos(Query query = null)
        {
            var items = Context.AfeccionGrupos.AsQueryable();

            items = items.Include(i => i.Patologium);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAfeccionGruposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAfeccionGrupoGet(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnGetAfeccionGrupoByCodAfeccionGrupo(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> GetAfeccionGrupoByCodAfeccionGrupo(int codafecciongrupo)
        {
            var items = Context.AfeccionGrupos
                              .AsNoTracking()
                              .Where(i => i.CodAfeccionGrupo == codafecciongrupo);

            items = items.Include(i => i.Patologium);
 
            OnGetAfeccionGrupoByCodAfeccionGrupo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAfeccionGrupoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAfeccionGrupoCreated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnAfterAfeccionGrupoCreated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> CreateAfeccionGrupo(SgpaNew.Server.Models.Sgpa.AfeccionGrupo afecciongrupo)
        {
            OnAfeccionGrupoCreated(afecciongrupo);

            var existingItem = Context.AfeccionGrupos
                              .Where(i => i.CodAfeccionGrupo == afecciongrupo.CodAfeccionGrupo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AfeccionGrupos.Add(afecciongrupo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(afecciongrupo).State = EntityState.Detached;
                throw;
            }

            OnAfterAfeccionGrupoCreated(afecciongrupo);

            return afecciongrupo;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> CancelAfeccionGrupoChanges(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAfeccionGrupoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnAfterAfeccionGrupoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> UpdateAfeccionGrupo(int codafecciongrupo, SgpaNew.Server.Models.Sgpa.AfeccionGrupo afecciongrupo)
        {
            OnAfeccionGrupoUpdated(afecciongrupo);

            var itemToUpdate = Context.AfeccionGrupos
                              .Where(i => i.CodAfeccionGrupo == afecciongrupo.CodAfeccionGrupo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(afecciongrupo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAfeccionGrupoUpdated(afecciongrupo);

            return afecciongrupo;
        }

        partial void OnAfeccionGrupoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);
        partial void OnAfterAfeccionGrupoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionGrupo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionGrupo> DeleteAfeccionGrupo(int codafecciongrupo)
        {
            var itemToDelete = Context.AfeccionGrupos
                              .Where(i => i.CodAfeccionGrupo == codafecciongrupo)
                              .Include(i => i.AfeccionTipos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAfeccionGrupoDeleted(itemToDelete);


            Context.AfeccionGrupos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAfeccionGrupoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAfeccionTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAfeccionTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afecciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afecciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAfeccionTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionTipo> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionTipo>> GetAfeccionTipos(Query query = null)
        {
            var items = Context.AfeccionTipos.AsQueryable();

            items = items.Include(i => i.AfeccionGrupo);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAfeccionTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAfeccionTipoGet(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnGetAfeccionTipoByCodAfeccionTipo(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfeccionTipo> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> GetAfeccionTipoByCodAfeccionTipo(short codafecciontipo)
        {
            var items = Context.AfeccionTipos
                              .AsNoTracking()
                              .Where(i => i.CodAfeccionTipo == codafecciontipo);

            items = items.Include(i => i.AfeccionGrupo);
 
            OnGetAfeccionTipoByCodAfeccionTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAfeccionTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAfeccionTipoCreated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnAfterAfeccionTipoCreated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> CreateAfeccionTipo(SgpaNew.Server.Models.Sgpa.AfeccionTipo afecciontipo)
        {
            OnAfeccionTipoCreated(afecciontipo);

            var existingItem = Context.AfeccionTipos
                              .Where(i => i.CodAfeccionTipo == afecciontipo.CodAfeccionTipo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AfeccionTipos.Add(afecciontipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(afecciontipo).State = EntityState.Detached;
                throw;
            }

            OnAfterAfeccionTipoCreated(afecciontipo);

            return afecciontipo;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> CancelAfeccionTipoChanges(SgpaNew.Server.Models.Sgpa.AfeccionTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAfeccionTipoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnAfterAfeccionTipoUpdated(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> UpdateAfeccionTipo(short codafecciontipo, SgpaNew.Server.Models.Sgpa.AfeccionTipo afecciontipo)
        {
            OnAfeccionTipoUpdated(afecciontipo);

            var itemToUpdate = Context.AfeccionTipos
                              .Where(i => i.CodAfeccionTipo == afecciontipo.CodAfeccionTipo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(afecciontipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAfeccionTipoUpdated(afecciontipo);

            return afecciontipo;
        }

        partial void OnAfeccionTipoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);
        partial void OnAfterAfeccionTipoDeleted(SgpaNew.Server.Models.Sgpa.AfeccionTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfeccionTipo> DeleteAfeccionTipo(short codafecciontipo)
        {
            var itemToDelete = Context.AfeccionTipos
                              .Where(i => i.CodAfeccionTipo == codafecciontipo)
                              .Include(i => i.Certificacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAfeccionTipoDeleted(itemToDelete);


            Context.AfeccionTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAfeccionTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAfiliadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAfiliadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAfiliadosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Afiliado> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Afiliado>> GetAfiliados(Query query = null)
        {
            var items = Context.Afiliados.AsQueryable();

            items = items.Include(i => i.Banco);
            items = items.Include(i => i.Mutualistum);
            items = items.Include(i => i.RegimenJubilatorio);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAfiliadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAfiliadoGet(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnGetAfiliadoByCi(ref IQueryable<SgpaNew.Server.Models.Sgpa.Afiliado> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> GetAfiliadoByCi(int ci)
        {
            var items = Context.Afiliados
                              .AsNoTracking()
                              .Where(i => i.CI == ci);

            items = items.Include(i => i.Banco);
            items = items.Include(i => i.Mutualistum);
            items = items.Include(i => i.RegimenJubilatorio);
 
            OnGetAfiliadoByCi(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAfiliadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAfiliadoCreated(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnAfterAfiliadoCreated(SgpaNew.Server.Models.Sgpa.Afiliado item);

        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> CreateAfiliado(SgpaNew.Server.Models.Sgpa.Afiliado afiliado)
        {
            OnAfiliadoCreated(afiliado);

            var existingItem = Context.Afiliados
                              .Where(i => i.CI == afiliado.CI)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Afiliados.Add(afiliado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(afiliado).State = EntityState.Detached;
                throw;
            }

            OnAfterAfiliadoCreated(afiliado);

            return afiliado;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> CancelAfiliadoChanges(SgpaNew.Server.Models.Sgpa.Afiliado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnAfterAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.Afiliado item);

        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> UpdateAfiliado(int ci, SgpaNew.Server.Models.Sgpa.Afiliado afiliado)
        {
            OnAfiliadoUpdated(afiliado);

            var itemToUpdate = Context.Afiliados
                              .Where(i => i.CI == afiliado.CI)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(afiliado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAfiliadoUpdated(afiliado);

            return afiliado;
        }

        partial void OnAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.Afiliado item);
        partial void OnAfterAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.Afiliado item);

        public async Task<SgpaNew.Server.Models.Sgpa.Afiliado> DeleteAfiliado(int ci)
        {
            var itemToDelete = Context.Afiliados
                              .Where(i => i.CI == ci)
                              .Include(i => i.AdPreJubs)
                              .Include(i => i.AfiliadoApuntes)
                              .Include(i => i.AfiliadoEspecialidads)
                              .Include(i => i.Certificacions)
                              .Include(i => i.CertificacionProrrogas)
                              .Include(i => i.Prestacions)
                              .Include(i => i.PrimaFallecimientos)
                              .Include(i => i.ReintegroMutuals)
                              .Include(i => i.SubsidioCabezals)
                              .Include(i => i.Trabajas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAfiliadoDeleted(itemToDelete);


            Context.Afiliados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAfiliadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAfiliadoApuntesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoapuntes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoapuntes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAfiliadoApuntesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoapuntes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoapuntes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAfiliadoApuntesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoApunte>> GetAfiliadoApuntes(Query query = null)
        {
            var items = Context.AfiliadoApuntes.AsQueryable();

            items = items.Include(i => i.Afiliado);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAfiliadoApuntesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAfiliadoApunteGet(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnGetAfiliadoApunteByAfiliadoApunteId(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> GetAfiliadoApunteByAfiliadoApunteId(int afiliadoapunteid)
        {
            var items = Context.AfiliadoApuntes
                              .AsNoTracking()
                              .Where(i => i.AfiliadoApunteId == afiliadoapunteid);

            items = items.Include(i => i.Afiliado);
 
            OnGetAfiliadoApunteByAfiliadoApunteId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAfiliadoApunteGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAfiliadoApunteCreated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnAfterAfiliadoApunteCreated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> CreateAfiliadoApunte(SgpaNew.Server.Models.Sgpa.AfiliadoApunte afiliadoapunte)
        {
            OnAfiliadoApunteCreated(afiliadoapunte);

            var existingItem = Context.AfiliadoApuntes
                              .Where(i => i.AfiliadoApunteId == afiliadoapunte.AfiliadoApunteId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AfiliadoApuntes.Add(afiliadoapunte);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(afiliadoapunte).State = EntityState.Detached;
                throw;
            }

            OnAfterAfiliadoApunteCreated(afiliadoapunte);

            return afiliadoapunte;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> CancelAfiliadoApunteChanges(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAfiliadoApunteUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnAfterAfiliadoApunteUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> UpdateAfiliadoApunte(int afiliadoapunteid, SgpaNew.Server.Models.Sgpa.AfiliadoApunte afiliadoapunte)
        {
            OnAfiliadoApunteUpdated(afiliadoapunte);

            var itemToUpdate = Context.AfiliadoApuntes
                              .Where(i => i.AfiliadoApunteId == afiliadoapunte.AfiliadoApunteId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(afiliadoapunte);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAfiliadoApunteUpdated(afiliadoapunte);

            return afiliadoapunte;
        }

        partial void OnAfiliadoApunteDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);
        partial void OnAfterAfiliadoApunteDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoApunte item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoApunte> DeleteAfiliadoApunte(int afiliadoapunteid)
        {
            var itemToDelete = Context.AfiliadoApuntes
                              .Where(i => i.AfiliadoApunteId == afiliadoapunteid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAfiliadoApunteDeleted(itemToDelete);


            Context.AfiliadoApuntes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAfiliadoApunteDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAfiliadoEspecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAfiliadoEspecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/afiliadoespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/afiliadoespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAfiliadoEspecialidadsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad>> GetAfiliadoEspecialidads(Query query = null)
        {
            var items = Context.AfiliadoEspecialidads.AsQueryable();

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.Especialidad);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAfiliadoEspecialidadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAfiliadoEspecialidadGet(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnGetAfiliadoEspecialidadByAfiliadoEspecialidadId(ref IQueryable<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> GetAfiliadoEspecialidadByAfiliadoEspecialidadId(int afiliadoespecialidadid)
        {
            var items = Context.AfiliadoEspecialidads
                              .AsNoTracking()
                              .Where(i => i.AfiliadoEspecialidadId == afiliadoespecialidadid);

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.Especialidad);
 
            OnGetAfiliadoEspecialidadByAfiliadoEspecialidadId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAfiliadoEspecialidadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAfiliadoEspecialidadCreated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnAfterAfiliadoEspecialidadCreated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> CreateAfiliadoEspecialidad(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad afiliadoespecialidad)
        {
            OnAfiliadoEspecialidadCreated(afiliadoespecialidad);

            var existingItem = Context.AfiliadoEspecialidads
                              .Where(i => i.AfiliadoEspecialidadId == afiliadoespecialidad.AfiliadoEspecialidadId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AfiliadoEspecialidads.Add(afiliadoespecialidad);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(afiliadoespecialidad).State = EntityState.Detached;
                throw;
            }

            OnAfterAfiliadoEspecialidadCreated(afiliadoespecialidad);

            return afiliadoespecialidad;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> CancelAfiliadoEspecialidadChanges(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAfiliadoEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnAfterAfiliadoEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> UpdateAfiliadoEspecialidad(int afiliadoespecialidadid, SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad afiliadoespecialidad)
        {
            OnAfiliadoEspecialidadUpdated(afiliadoespecialidad);

            var itemToUpdate = Context.AfiliadoEspecialidads
                              .Where(i => i.AfiliadoEspecialidadId == afiliadoespecialidad.AfiliadoEspecialidadId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(afiliadoespecialidad);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAfiliadoEspecialidadUpdated(afiliadoespecialidad);

            return afiliadoespecialidad;
        }

        partial void OnAfiliadoEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);
        partial void OnAfterAfiliadoEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad item);

        public async Task<SgpaNew.Server.Models.Sgpa.AfiliadoEspecialidad> DeleteAfiliadoEspecialidad(int afiliadoespecialidadid)
        {
            var itemToDelete = Context.AfiliadoEspecialidads
                              .Where(i => i.AfiliadoEspecialidadId == afiliadoespecialidadid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAfiliadoEspecialidadDeleted(itemToDelete);


            Context.AfiliadoEspecialidads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAfiliadoEspecialidadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAporteTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/aportetipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/aportetipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAporteTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/aportetipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/aportetipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAporteTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.AporteTipo> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.AporteTipo>> GetAporteTipos(Query query = null)
        {
            var items = Context.AporteTipos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAporteTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAporteTipoGet(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnGetAporteTipoByCodAporteTipo(ref IQueryable<SgpaNew.Server.Models.Sgpa.AporteTipo> items);


        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> GetAporteTipoByCodAporteTipo(string codaportetipo)
        {
            var items = Context.AporteTipos
                              .AsNoTracking()
                              .Where(i => i.CodAporteTipo == codaportetipo);

 
            OnGetAporteTipoByCodAporteTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAporteTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAporteTipoCreated(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnAfterAporteTipoCreated(SgpaNew.Server.Models.Sgpa.AporteTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> CreateAporteTipo(SgpaNew.Server.Models.Sgpa.AporteTipo aportetipo)
        {
            OnAporteTipoCreated(aportetipo);

            var existingItem = Context.AporteTipos
                              .Where(i => i.CodAporteTipo == aportetipo.CodAporteTipo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AporteTipos.Add(aportetipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(aportetipo).State = EntityState.Detached;
                throw;
            }

            OnAfterAporteTipoCreated(aportetipo);

            return aportetipo;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> CancelAporteTipoChanges(SgpaNew.Server.Models.Sgpa.AporteTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAporteTipoUpdated(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnAfterAporteTipoUpdated(SgpaNew.Server.Models.Sgpa.AporteTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> UpdateAporteTipo(string codaportetipo, SgpaNew.Server.Models.Sgpa.AporteTipo aportetipo)
        {
            OnAporteTipoUpdated(aportetipo);

            var itemToUpdate = Context.AporteTipos
                              .Where(i => i.CodAporteTipo == aportetipo.CodAporteTipo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(aportetipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAporteTipoUpdated(aportetipo);

            return aportetipo;
        }

        partial void OnAporteTipoDeleted(SgpaNew.Server.Models.Sgpa.AporteTipo item);
        partial void OnAfterAporteTipoDeleted(SgpaNew.Server.Models.Sgpa.AporteTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.AporteTipo> DeleteAporteTipo(string codaportetipo)
        {
            var itemToDelete = Context.AporteTipos
                              .Where(i => i.CodAporteTipo == codaportetipo)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAporteTipoDeleted(itemToDelete);


            Context.AporteTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAporteTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBajaMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBajaMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBajaMotivosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.BajaMotivo> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.BajaMotivo>> GetBajaMotivos(Query query = null)
        {
            var items = Context.BajaMotivos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnBajaMotivosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBajaMotivoGet(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnGetBajaMotivoByCodBajaMotivo(ref IQueryable<SgpaNew.Server.Models.Sgpa.BajaMotivo> items);


        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> GetBajaMotivoByCodBajaMotivo(int codbajamotivo)
        {
            var items = Context.BajaMotivos
                              .AsNoTracking()
                              .Where(i => i.CodBajaMotivo == codbajamotivo);

 
            OnGetBajaMotivoByCodBajaMotivo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBajaMotivoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBajaMotivoCreated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnAfterBajaMotivoCreated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);

        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> CreateBajaMotivo(SgpaNew.Server.Models.Sgpa.BajaMotivo bajamotivo)
        {
            OnBajaMotivoCreated(bajamotivo);

            var existingItem = Context.BajaMotivos
                              .Where(i => i.CodBajaMotivo == bajamotivo.CodBajaMotivo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BajaMotivos.Add(bajamotivo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bajamotivo).State = EntityState.Detached;
                throw;
            }

            OnAfterBajaMotivoCreated(bajamotivo);

            return bajamotivo;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> CancelBajaMotivoChanges(SgpaNew.Server.Models.Sgpa.BajaMotivo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBajaMotivoUpdated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnAfterBajaMotivoUpdated(SgpaNew.Server.Models.Sgpa.BajaMotivo item);

        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> UpdateBajaMotivo(int codbajamotivo, SgpaNew.Server.Models.Sgpa.BajaMotivo bajamotivo)
        {
            OnBajaMotivoUpdated(bajamotivo);

            var itemToUpdate = Context.BajaMotivos
                              .Where(i => i.CodBajaMotivo == bajamotivo.CodBajaMotivo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bajamotivo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBajaMotivoUpdated(bajamotivo);

            return bajamotivo;
        }

        partial void OnBajaMotivoDeleted(SgpaNew.Server.Models.Sgpa.BajaMotivo item);
        partial void OnAfterBajaMotivoDeleted(SgpaNew.Server.Models.Sgpa.BajaMotivo item);

        public async Task<SgpaNew.Server.Models.Sgpa.BajaMotivo> DeleteBajaMotivo(int codbajamotivo)
        {
            var itemToDelete = Context.BajaMotivos
                              .Where(i => i.CodBajaMotivo == codbajamotivo)
                              .Include(i => i.Trabajas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBajaMotivoDeleted(itemToDelete);


            Context.BajaMotivos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBajaMotivoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBancosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBancosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBancosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Banco> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Banco>> GetBancos(Query query = null)
        {
            var items = Context.Bancos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnBancosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBancoGet(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnGetBancoByCodBanco(ref IQueryable<SgpaNew.Server.Models.Sgpa.Banco> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Banco> GetBancoByCodBanco(int codbanco)
        {
            var items = Context.Bancos
                              .AsNoTracking()
                              .Where(i => i.CodBanco == codbanco);

 
            OnGetBancoByCodBanco(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBancoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBancoCreated(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnAfterBancoCreated(SgpaNew.Server.Models.Sgpa.Banco item);

        public async Task<SgpaNew.Server.Models.Sgpa.Banco> CreateBanco(SgpaNew.Server.Models.Sgpa.Banco banco)
        {
            OnBancoCreated(banco);

            var existingItem = Context.Bancos
                              .Where(i => i.CodBanco == banco.CodBanco)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Bancos.Add(banco);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(banco).State = EntityState.Detached;
                throw;
            }

            OnAfterBancoCreated(banco);

            return banco;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Banco> CancelBancoChanges(SgpaNew.Server.Models.Sgpa.Banco item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBancoUpdated(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnAfterBancoUpdated(SgpaNew.Server.Models.Sgpa.Banco item);

        public async Task<SgpaNew.Server.Models.Sgpa.Banco> UpdateBanco(int codbanco, SgpaNew.Server.Models.Sgpa.Banco banco)
        {
            OnBancoUpdated(banco);

            var itemToUpdate = Context.Bancos
                              .Where(i => i.CodBanco == banco.CodBanco)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(banco);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBancoUpdated(banco);

            return banco;
        }

        partial void OnBancoDeleted(SgpaNew.Server.Models.Sgpa.Banco item);
        partial void OnAfterBancoDeleted(SgpaNew.Server.Models.Sgpa.Banco item);

        public async Task<SgpaNew.Server.Models.Sgpa.Banco> DeleteBanco(int codbanco)
        {
            var itemToDelete = Context.Bancos
                              .Where(i => i.CodBanco == codbanco)
                              .Include(i => i.Afiliados)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBancoDeleted(itemToDelete);


            Context.Bancos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBancoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCertificacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCertificacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCertificacionsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Certificacion> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Certificacion>> GetCertificacions(Query query = null)
        {
            var items = Context.Certificacions.AsQueryable();

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.AfeccionTipo);
            items = items.Include(i => i.Certificador);
            items = items.Include(i => i.SalidaTipo);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCertificacionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCertificacionGet(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnGetCertificacionByNroLlamado(ref IQueryable<SgpaNew.Server.Models.Sgpa.Certificacion> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> GetCertificacionByNroLlamado(int nrollamado)
        {
            var items = Context.Certificacions
                              .AsNoTracking()
                              .Where(i => i.NroLlamado == nrollamado);

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.AfeccionTipo);
            items = items.Include(i => i.Certificador);
            items = items.Include(i => i.SalidaTipo);
 
            OnGetCertificacionByNroLlamado(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCertificacionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCertificacionCreated(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnAfterCertificacionCreated(SgpaNew.Server.Models.Sgpa.Certificacion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> CreateCertificacion(SgpaNew.Server.Models.Sgpa.Certificacion certificacion)
        {
            OnCertificacionCreated(certificacion);

            var existingItem = Context.Certificacions
                              .Where(i => i.NroLlamado == certificacion.NroLlamado)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Certificacions.Add(certificacion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(certificacion).State = EntityState.Detached;
                throw;
            }

            OnAfterCertificacionCreated(certificacion);

            return certificacion;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> CancelCertificacionChanges(SgpaNew.Server.Models.Sgpa.Certificacion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCertificacionUpdated(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnAfterCertificacionUpdated(SgpaNew.Server.Models.Sgpa.Certificacion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> UpdateCertificacion(int nrollamado, SgpaNew.Server.Models.Sgpa.Certificacion certificacion)
        {
            OnCertificacionUpdated(certificacion);

            var itemToUpdate = Context.Certificacions
                              .Where(i => i.NroLlamado == certificacion.NroLlamado)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(certificacion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCertificacionUpdated(certificacion);

            return certificacion;
        }

        partial void OnCertificacionDeleted(SgpaNew.Server.Models.Sgpa.Certificacion item);
        partial void OnAfterCertificacionDeleted(SgpaNew.Server.Models.Sgpa.Certificacion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificacion> DeleteCertificacion(int nrollamado)
        {
            var itemToDelete = Context.Certificacions
                              .Where(i => i.NroLlamado == nrollamado)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCertificacionDeleted(itemToDelete);


            Context.Certificacions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCertificacionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCertificacionProrrogasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacionprorrogas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacionprorrogas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCertificacionProrrogasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificacionprorrogas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificacionprorrogas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCertificacionProrrogasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.CertificacionProrroga>> GetCertificacionProrrogas(Query query = null)
        {
            var items = Context.CertificacionProrrogas.AsQueryable();

            items = items.Include(i => i.Afiliado);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCertificacionProrrogasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCertificacionProrrogaGet(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnGetCertificacionProrrogaByCertificacionProrrogaId(ref IQueryable<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> items);


        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> GetCertificacionProrrogaByCertificacionProrrogaId(int certificacionprorrogaid)
        {
            var items = Context.CertificacionProrrogas
                              .AsNoTracking()
                              .Where(i => i.CertificacionProrrogaId == certificacionprorrogaid);

            items = items.Include(i => i.Afiliado);
 
            OnGetCertificacionProrrogaByCertificacionProrrogaId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCertificacionProrrogaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCertificacionProrrogaCreated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnAfterCertificacionProrrogaCreated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);

        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> CreateCertificacionProrroga(SgpaNew.Server.Models.Sgpa.CertificacionProrroga certificacionprorroga)
        {
            OnCertificacionProrrogaCreated(certificacionprorroga);

            var existingItem = Context.CertificacionProrrogas
                              .Where(i => i.CertificacionProrrogaId == certificacionprorroga.CertificacionProrrogaId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CertificacionProrrogas.Add(certificacionprorroga);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(certificacionprorroga).State = EntityState.Detached;
                throw;
            }

            OnAfterCertificacionProrrogaCreated(certificacionprorroga);

            return certificacionprorroga;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> CancelCertificacionProrrogaChanges(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCertificacionProrrogaUpdated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnAfterCertificacionProrrogaUpdated(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);

        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> UpdateCertificacionProrroga(int certificacionprorrogaid, SgpaNew.Server.Models.Sgpa.CertificacionProrroga certificacionprorroga)
        {
            OnCertificacionProrrogaUpdated(certificacionprorroga);

            var itemToUpdate = Context.CertificacionProrrogas
                              .Where(i => i.CertificacionProrrogaId == certificacionprorroga.CertificacionProrrogaId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(certificacionprorroga);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCertificacionProrrogaUpdated(certificacionprorroga);

            return certificacionprorroga;
        }

        partial void OnCertificacionProrrogaDeleted(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);
        partial void OnAfterCertificacionProrrogaDeleted(SgpaNew.Server.Models.Sgpa.CertificacionProrroga item);

        public async Task<SgpaNew.Server.Models.Sgpa.CertificacionProrroga> DeleteCertificacionProrroga(int certificacionprorrogaid)
        {
            var itemToDelete = Context.CertificacionProrrogas
                              .Where(i => i.CertificacionProrrogaId == certificacionprorrogaid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCertificacionProrrogaDeleted(itemToDelete);


            Context.CertificacionProrrogas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCertificacionProrrogaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCertificadorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCertificadorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/certificadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/certificadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCertificadorsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Certificador> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Certificador>> GetCertificadors(Query query = null)
        {
            var items = Context.Certificadors.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCertificadorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCertificadorGet(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnGetCertificadorByCodCertificador(ref IQueryable<SgpaNew.Server.Models.Sgpa.Certificador> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> GetCertificadorByCodCertificador(short codcertificador)
        {
            var items = Context.Certificadors
                              .AsNoTracking()
                              .Where(i => i.CodCertificador == codcertificador);

 
            OnGetCertificadorByCodCertificador(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCertificadorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCertificadorCreated(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnAfterCertificadorCreated(SgpaNew.Server.Models.Sgpa.Certificador item);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> CreateCertificador(SgpaNew.Server.Models.Sgpa.Certificador certificador)
        {
            OnCertificadorCreated(certificador);

            var existingItem = Context.Certificadors
                              .Where(i => i.CodCertificador == certificador.CodCertificador)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Certificadors.Add(certificador);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(certificador).State = EntityState.Detached;
                throw;
            }

            OnAfterCertificadorCreated(certificador);

            return certificador;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> CancelCertificadorChanges(SgpaNew.Server.Models.Sgpa.Certificador item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCertificadorUpdated(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnAfterCertificadorUpdated(SgpaNew.Server.Models.Sgpa.Certificador item);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> UpdateCertificador(short codcertificador, SgpaNew.Server.Models.Sgpa.Certificador certificador)
        {
            OnCertificadorUpdated(certificador);

            var itemToUpdate = Context.Certificadors
                              .Where(i => i.CodCertificador == certificador.CodCertificador)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(certificador);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCertificadorUpdated(certificador);

            return certificador;
        }

        partial void OnCertificadorDeleted(SgpaNew.Server.Models.Sgpa.Certificador item);
        partial void OnAfterCertificadorDeleted(SgpaNew.Server.Models.Sgpa.Certificador item);

        public async Task<SgpaNew.Server.Models.Sgpa.Certificador> DeleteCertificador(short codcertificador)
        {
            var itemToDelete = Context.Certificadors
                              .Where(i => i.CodCertificador == codcertificador)
                              .Include(i => i.Certificacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCertificadorDeleted(itemToDelete);


            Context.Certificadors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCertificadorDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCtasbrousToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/ctasbrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/ctasbrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCtasbrousToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/ctasbrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/ctasbrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCtasbrousRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Ctasbrou> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Ctasbrou>> GetCtasbrous(Query query = null)
        {
            var items = Context.Ctasbrous.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCtasbrousRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCtasbrouGet(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnGetCtasbrouByCi(ref IQueryable<SgpaNew.Server.Models.Sgpa.Ctasbrou> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> GetCtasbrouByCi(int ci)
        {
            var items = Context.Ctasbrous
                              .AsNoTracking()
                              .Where(i => i.CI == ci);

 
            OnGetCtasbrouByCi(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCtasbrouGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCtasbrouCreated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnAfterCtasbrouCreated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);

        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> CreateCtasbrou(SgpaNew.Server.Models.Sgpa.Ctasbrou ctasbrou)
        {
            OnCtasbrouCreated(ctasbrou);

            var existingItem = Context.Ctasbrous
                              .Where(i => i.CI == ctasbrou.CI)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Ctasbrous.Add(ctasbrou);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ctasbrou).State = EntityState.Detached;
                throw;
            }

            OnAfterCtasbrouCreated(ctasbrou);

            return ctasbrou;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> CancelCtasbrouChanges(SgpaNew.Server.Models.Sgpa.Ctasbrou item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCtasbrouUpdated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnAfterCtasbrouUpdated(SgpaNew.Server.Models.Sgpa.Ctasbrou item);

        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> UpdateCtasbrou(int ci, SgpaNew.Server.Models.Sgpa.Ctasbrou ctasbrou)
        {
            OnCtasbrouUpdated(ctasbrou);

            var itemToUpdate = Context.Ctasbrous
                              .Where(i => i.CI == ctasbrou.CI)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ctasbrou);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCtasbrouUpdated(ctasbrou);

            return ctasbrou;
        }

        partial void OnCtasbrouDeleted(SgpaNew.Server.Models.Sgpa.Ctasbrou item);
        partial void OnAfterCtasbrouDeleted(SgpaNew.Server.Models.Sgpa.Ctasbrou item);

        public async Task<SgpaNew.Server.Models.Sgpa.Ctasbrou> DeleteCtasbrou(int ci)
        {
            var itemToDelete = Context.Ctasbrous
                              .Where(i => i.CI == ci)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCtasbrouDeleted(itemToDelete);


            Context.Ctasbrous.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCtasbrouDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCuentaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/cuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/cuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuentaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/cuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/cuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuentaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Cuentum> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Cuentum>> GetCuenta(Query query = null)
        {
            var items = Context.Cuenta.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCuentaRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportDepartamentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepartamentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepartamentosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Departamento> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Departamento>> GetDepartamentos(Query query = null)
        {
            var items = Context.Departamentos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDepartamentosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepartamentoGet(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnGetDepartamentoByCodDepartamento(ref IQueryable<SgpaNew.Server.Models.Sgpa.Departamento> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> GetDepartamentoByCodDepartamento(string coddepartamento)
        {
            var items = Context.Departamentos
                              .AsNoTracking()
                              .Where(i => i.CodDepartamento == coddepartamento);

 
            OnGetDepartamentoByCodDepartamento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepartamentoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepartamentoCreated(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnAfterDepartamentoCreated(SgpaNew.Server.Models.Sgpa.Departamento item);

        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> CreateDepartamento(SgpaNew.Server.Models.Sgpa.Departamento departamento)
        {
            OnDepartamentoCreated(departamento);

            var existingItem = Context.Departamentos
                              .Where(i => i.CodDepartamento == departamento.CodDepartamento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Departamentos.Add(departamento);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(departamento).State = EntityState.Detached;
                throw;
            }

            OnAfterDepartamentoCreated(departamento);

            return departamento;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> CancelDepartamentoChanges(SgpaNew.Server.Models.Sgpa.Departamento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepartamentoUpdated(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnAfterDepartamentoUpdated(SgpaNew.Server.Models.Sgpa.Departamento item);

        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> UpdateDepartamento(string coddepartamento, SgpaNew.Server.Models.Sgpa.Departamento departamento)
        {
            OnDepartamentoUpdated(departamento);

            var itemToUpdate = Context.Departamentos
                              .Where(i => i.CodDepartamento == departamento.CodDepartamento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(departamento);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepartamentoUpdated(departamento);

            return departamento;
        }

        partial void OnDepartamentoDeleted(SgpaNew.Server.Models.Sgpa.Departamento item);
        partial void OnAfterDepartamentoDeleted(SgpaNew.Server.Models.Sgpa.Departamento item);

        public async Task<SgpaNew.Server.Models.Sgpa.Departamento> DeleteDepartamento(string coddepartamento)
        {
            var itemToDelete = Context.Departamentos
                              .Where(i => i.CodDepartamento == coddepartamento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepartamentoDeleted(itemToDelete);


            Context.Departamentos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepartamentoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDiscountsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/discounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/discounts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDiscountsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/discounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/discounts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDiscountsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Discount> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Discount>> GetDiscounts(Query query = null)
        {
            var items = Context.Discounts.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDiscountsRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportEmpresasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmpresasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmpresasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Empresa> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Empresa>> GetEmpresas(Query query = null)
        {
            var items = Context.Empresas.AsQueryable();

            items = items.Include(i => i.RegimenAporte);
            items = items.Include(i => i.SituacionPago);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmpresasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmpresaGet(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnGetEmpresaByCodEmpresa(ref IQueryable<SgpaNew.Server.Models.Sgpa.Empresa> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> GetEmpresaByCodEmpresa(short codempresa)
        {
            var items = Context.Empresas
                              .AsNoTracking()
                              .Where(i => i.CodEmpresa == codempresa);

            items = items.Include(i => i.RegimenAporte);
            items = items.Include(i => i.SituacionPago);
 
            OnGetEmpresaByCodEmpresa(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmpresaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmpresaCreated(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnAfterEmpresaCreated(SgpaNew.Server.Models.Sgpa.Empresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> CreateEmpresa(SgpaNew.Server.Models.Sgpa.Empresa empresa)
        {
            OnEmpresaCreated(empresa);

            var existingItem = Context.Empresas
                              .Where(i => i.CodEmpresa == empresa.CodEmpresa)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Empresas.Add(empresa);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(empresa).State = EntityState.Detached;
                throw;
            }

            OnAfterEmpresaCreated(empresa);

            return empresa;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> CancelEmpresaChanges(SgpaNew.Server.Models.Sgpa.Empresa item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmpresaUpdated(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnAfterEmpresaUpdated(SgpaNew.Server.Models.Sgpa.Empresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> UpdateEmpresa(short codempresa, SgpaNew.Server.Models.Sgpa.Empresa empresa)
        {
            OnEmpresaUpdated(empresa);

            var itemToUpdate = Context.Empresas
                              .Where(i => i.CodEmpresa == empresa.CodEmpresa)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(empresa);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmpresaUpdated(empresa);

            return empresa;
        }

        partial void OnEmpresaDeleted(SgpaNew.Server.Models.Sgpa.Empresa item);
        partial void OnAfterEmpresaDeleted(SgpaNew.Server.Models.Sgpa.Empresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.Empresa> DeleteEmpresa(short codempresa)
        {
            var itemToDelete = Context.Empresas
                              .Where(i => i.CodEmpresa == codempresa)
                              .Include(i => i.EmpresaPagos)
                              .Include(i => i.SubsidioCabezalEmpresas)
                              .Include(i => i.SubsidioItemEmpresas)
                              .Include(i => i.Trabajas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmpresaDeleted(itemToDelete);


            Context.Empresas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmpresaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmpresaPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmpresaPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/empresapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/empresapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmpresaPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.EmpresaPago> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.EmpresaPago>> GetEmpresaPagos(Query query = null)
        {
            var items = Context.EmpresaPagos.AsQueryable();

            items = items.Include(i => i.Empresa);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEmpresaPagosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmpresaPagoGet(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnGetEmpresaPagoByEmpresaPagoId(ref IQueryable<SgpaNew.Server.Models.Sgpa.EmpresaPago> items);


        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> GetEmpresaPagoByEmpresaPagoId(int empresapagoid)
        {
            var items = Context.EmpresaPagos
                              .AsNoTracking()
                              .Where(i => i.EmpresaPagoId == empresapagoid);

            items = items.Include(i => i.Empresa);
 
            OnGetEmpresaPagoByEmpresaPagoId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmpresaPagoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmpresaPagoCreated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnAfterEmpresaPagoCreated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> CreateEmpresaPago(SgpaNew.Server.Models.Sgpa.EmpresaPago empresapago)
        {
            OnEmpresaPagoCreated(empresapago);

            var existingItem = Context.EmpresaPagos
                              .Where(i => i.EmpresaPagoId == empresapago.EmpresaPagoId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.EmpresaPagos.Add(empresapago);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(empresapago).State = EntityState.Detached;
                throw;
            }

            OnAfterEmpresaPagoCreated(empresapago);

            return empresapago;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> CancelEmpresaPagoChanges(SgpaNew.Server.Models.Sgpa.EmpresaPago item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmpresaPagoUpdated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnAfterEmpresaPagoUpdated(SgpaNew.Server.Models.Sgpa.EmpresaPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> UpdateEmpresaPago(int empresapagoid, SgpaNew.Server.Models.Sgpa.EmpresaPago empresapago)
        {
            OnEmpresaPagoUpdated(empresapago);

            var itemToUpdate = Context.EmpresaPagos
                              .Where(i => i.EmpresaPagoId == empresapago.EmpresaPagoId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(empresapago);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmpresaPagoUpdated(empresapago);

            return empresapago;
        }

        partial void OnEmpresaPagoDeleted(SgpaNew.Server.Models.Sgpa.EmpresaPago item);
        partial void OnAfterEmpresaPagoDeleted(SgpaNew.Server.Models.Sgpa.EmpresaPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.EmpresaPago> DeleteEmpresaPago(int empresapagoid)
        {
            var itemToDelete = Context.EmpresaPagos
                              .Where(i => i.EmpresaPagoId == empresapagoid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmpresaPagoDeleted(itemToDelete);


            Context.EmpresaPagos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmpresaPagoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEspecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEspecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEspecialidadsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Especialidad> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Especialidad>> GetEspecialidads(Query query = null)
        {
            var items = Context.Especialidads.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEspecialidadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEspecialidadGet(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnGetEspecialidadByCodEspecialidad(ref IQueryable<SgpaNew.Server.Models.Sgpa.Especialidad> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> GetEspecialidadByCodEspecialidad(int codespecialidad)
        {
            var items = Context.Especialidads
                              .AsNoTracking()
                              .Where(i => i.CodEspecialidad == codespecialidad);

 
            OnGetEspecialidadByCodEspecialidad(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEspecialidadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEspecialidadCreated(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnAfterEspecialidadCreated(SgpaNew.Server.Models.Sgpa.Especialidad item);

        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> CreateEspecialidad(SgpaNew.Server.Models.Sgpa.Especialidad especialidad)
        {
            OnEspecialidadCreated(especialidad);

            var existingItem = Context.Especialidads
                              .Where(i => i.CodEspecialidad == especialidad.CodEspecialidad)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Especialidads.Add(especialidad);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(especialidad).State = EntityState.Detached;
                throw;
            }

            OnAfterEspecialidadCreated(especialidad);

            return especialidad;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> CancelEspecialidadChanges(SgpaNew.Server.Models.Sgpa.Especialidad item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnAfterEspecialidadUpdated(SgpaNew.Server.Models.Sgpa.Especialidad item);

        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> UpdateEspecialidad(int codespecialidad, SgpaNew.Server.Models.Sgpa.Especialidad especialidad)
        {
            OnEspecialidadUpdated(especialidad);

            var itemToUpdate = Context.Especialidads
                              .Where(i => i.CodEspecialidad == especialidad.CodEspecialidad)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(especialidad);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEspecialidadUpdated(especialidad);

            return especialidad;
        }

        partial void OnEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.Especialidad item);
        partial void OnAfterEspecialidadDeleted(SgpaNew.Server.Models.Sgpa.Especialidad item);

        public async Task<SgpaNew.Server.Models.Sgpa.Especialidad> DeleteEspecialidad(int codespecialidad)
        {
            var itemToDelete = Context.Especialidads
                              .Where(i => i.CodEspecialidad == codespecialidad)
                              .Include(i => i.AfiliadoEspecialidads)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEspecialidadDeleted(itemToDelete);


            Context.Especialidads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEspecialidadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFormaPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/formapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/formapagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFormaPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/formapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/formapagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFormaPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.FormaPago> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.FormaPago>> GetFormaPagos(Query query = null)
        {
            var items = Context.FormaPagos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnFormaPagosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFormaPagoGet(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnGetFormaPagoByCodFormaPago(ref IQueryable<SgpaNew.Server.Models.Sgpa.FormaPago> items);


        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> GetFormaPagoByCodFormaPago(short codformapago)
        {
            var items = Context.FormaPagos
                              .AsNoTracking()
                              .Where(i => i.CodFormaPago == codformapago);

 
            OnGetFormaPagoByCodFormaPago(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFormaPagoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFormaPagoCreated(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnAfterFormaPagoCreated(SgpaNew.Server.Models.Sgpa.FormaPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> CreateFormaPago(SgpaNew.Server.Models.Sgpa.FormaPago formapago)
        {
            OnFormaPagoCreated(formapago);

            var existingItem = Context.FormaPagos
                              .Where(i => i.CodFormaPago == formapago.CodFormaPago)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.FormaPagos.Add(formapago);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(formapago).State = EntityState.Detached;
                throw;
            }

            OnAfterFormaPagoCreated(formapago);

            return formapago;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> CancelFormaPagoChanges(SgpaNew.Server.Models.Sgpa.FormaPago item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFormaPagoUpdated(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnAfterFormaPagoUpdated(SgpaNew.Server.Models.Sgpa.FormaPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> UpdateFormaPago(short codformapago, SgpaNew.Server.Models.Sgpa.FormaPago formapago)
        {
            OnFormaPagoUpdated(formapago);

            var itemToUpdate = Context.FormaPagos
                              .Where(i => i.CodFormaPago == formapago.CodFormaPago)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(formapago);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFormaPagoUpdated(formapago);

            return formapago;
        }

        partial void OnFormaPagoDeleted(SgpaNew.Server.Models.Sgpa.FormaPago item);
        partial void OnAfterFormaPagoDeleted(SgpaNew.Server.Models.Sgpa.FormaPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.FormaPago> DeleteFormaPago(short codformapago)
        {
            var itemToDelete = Context.FormaPagos
                              .Where(i => i.CodFormaPago == codformapago)
                              .Include(i => i.Mutualista)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFormaPagoDeleted(itemToDelete);


            Context.FormaPagos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFormaPagoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFranjaIrpfsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/franjairpfs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/franjairpfs/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFranjaIrpfsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/franjairpfs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/franjairpfs/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFranjaIrpfsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.FranjaIrpf> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.FranjaIrpf>> GetFranjaIrpfs(Query query = null)
        {
            var items = Context.FranjaIrpfs.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnFranjaIrpfsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFranjaIrpfGet(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnGetFranjaIrpfByDesde(ref IQueryable<SgpaNew.Server.Models.Sgpa.FranjaIrpf> items);


        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> GetFranjaIrpfByDesde(double desde)
        {
            var items = Context.FranjaIrpfs
                              .AsNoTracking()
                              .Where(i => i.Desde == desde);

 
            OnGetFranjaIrpfByDesde(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFranjaIrpfGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFranjaIrpfCreated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnAfterFranjaIrpfCreated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);

        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> CreateFranjaIrpf(SgpaNew.Server.Models.Sgpa.FranjaIrpf franjairpf)
        {
            OnFranjaIrpfCreated(franjairpf);

            var existingItem = Context.FranjaIrpfs
                              .Where(i => i.Desde == franjairpf.Desde)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.FranjaIrpfs.Add(franjairpf);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(franjairpf).State = EntityState.Detached;
                throw;
            }

            OnAfterFranjaIrpfCreated(franjairpf);

            return franjairpf;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> CancelFranjaIrpfChanges(SgpaNew.Server.Models.Sgpa.FranjaIrpf item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFranjaIrpfUpdated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnAfterFranjaIrpfUpdated(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);

        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> UpdateFranjaIrpf(double desde, SgpaNew.Server.Models.Sgpa.FranjaIrpf franjairpf)
        {
            OnFranjaIrpfUpdated(franjairpf);

            var itemToUpdate = Context.FranjaIrpfs
                              .Where(i => i.Desde == franjairpf.Desde)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(franjairpf);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFranjaIrpfUpdated(franjairpf);

            return franjairpf;
        }

        partial void OnFranjaIrpfDeleted(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);
        partial void OnAfterFranjaIrpfDeleted(SgpaNew.Server.Models.Sgpa.FranjaIrpf item);

        public async Task<SgpaNew.Server.Models.Sgpa.FranjaIrpf> DeleteFranjaIrpf(double desde)
        {
            var itemToDelete = Context.FranjaIrpfs
                              .Where(i => i.Desde == desde)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFranjaIrpfDeleted(itemToDelete);


            Context.FranjaIrpfs.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFranjaIrpfDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportImpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportImpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnImpsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Imp> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Imp>> GetImps(Query query = null)
        {
            var items = Context.Imps.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnImpsRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportImponiblesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportImponiblesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/imponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/imponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnImponiblesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Imponible> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Imponible>> GetImponibles(Query query = null)
        {
            var items = Context.Imponibles.AsQueryable();

            items = items.Include(i => i.Trabaja);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnImponiblesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnImponibleGet(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnGetImponibleByImponibleId(ref IQueryable<SgpaNew.Server.Models.Sgpa.Imponible> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> GetImponibleByImponibleId(int imponibleid)
        {
            var items = Context.Imponibles
                              .AsNoTracking()
                              .Where(i => i.ImponibleId == imponibleid);

            items = items.Include(i => i.Trabaja);
 
            OnGetImponibleByImponibleId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnImponibleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnImponibleCreated(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnAfterImponibleCreated(SgpaNew.Server.Models.Sgpa.Imponible item);

        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> CreateImponible(SgpaNew.Server.Models.Sgpa.Imponible imponible)
        {
            OnImponibleCreated(imponible);

            var existingItem = Context.Imponibles
                              .Where(i => i.ImponibleId == imponible.ImponibleId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Imponibles.Add(imponible);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(imponible).State = EntityState.Detached;
                throw;
            }

            OnAfterImponibleCreated(imponible);

            return imponible;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> CancelImponibleChanges(SgpaNew.Server.Models.Sgpa.Imponible item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnImponibleUpdated(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnAfterImponibleUpdated(SgpaNew.Server.Models.Sgpa.Imponible item);

        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> UpdateImponible(int imponibleid, SgpaNew.Server.Models.Sgpa.Imponible imponible)
        {
            OnImponibleUpdated(imponible);

            var itemToUpdate = Context.Imponibles
                              .Where(i => i.ImponibleId == imponible.ImponibleId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(imponible);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterImponibleUpdated(imponible);

            return imponible;
        }

        partial void OnImponibleDeleted(SgpaNew.Server.Models.Sgpa.Imponible item);
        partial void OnAfterImponibleDeleted(SgpaNew.Server.Models.Sgpa.Imponible item);

        public async Task<SgpaNew.Server.Models.Sgpa.Imponible> DeleteImponible(int imponibleid)
        {
            var itemToDelete = Context.Imponibles
                              .Where(i => i.ImponibleId == imponibleid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnImponibleDeleted(itemToDelete);


            Context.Imponibles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterImponibleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportInformeEstadisticosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/informeestadisticos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/informeestadisticos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportInformeEstadisticosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/informeestadisticos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/informeestadisticos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnInformeEstadisticosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.InformeEstadistico> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.InformeEstadistico>> GetInformeEstadisticos(Query query = null)
        {
            var items = Context.InformeEstadisticos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnInformeEstadisticosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnInformeEstadisticoGet(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnGetInformeEstadisticoByIdRpt(ref IQueryable<SgpaNew.Server.Models.Sgpa.InformeEstadistico> items);


        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> GetInformeEstadisticoByIdRpt(int idrpt)
        {
            var items = Context.InformeEstadisticos
                              .AsNoTracking()
                              .Where(i => i.IdRpt == idrpt);

 
            OnGetInformeEstadisticoByIdRpt(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnInformeEstadisticoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnInformeEstadisticoCreated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnAfterInformeEstadisticoCreated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);

        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> CreateInformeEstadistico(SgpaNew.Server.Models.Sgpa.InformeEstadistico informeestadistico)
        {
            OnInformeEstadisticoCreated(informeestadistico);

            var existingItem = Context.InformeEstadisticos
                              .Where(i => i.IdRpt == informeestadistico.IdRpt)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.InformeEstadisticos.Add(informeestadistico);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(informeestadistico).State = EntityState.Detached;
                throw;
            }

            OnAfterInformeEstadisticoCreated(informeestadistico);

            return informeestadistico;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> CancelInformeEstadisticoChanges(SgpaNew.Server.Models.Sgpa.InformeEstadistico item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnInformeEstadisticoUpdated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnAfterInformeEstadisticoUpdated(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);

        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> UpdateInformeEstadistico(int idrpt, SgpaNew.Server.Models.Sgpa.InformeEstadistico informeestadistico)
        {
            OnInformeEstadisticoUpdated(informeestadistico);

            var itemToUpdate = Context.InformeEstadisticos
                              .Where(i => i.IdRpt == informeestadistico.IdRpt)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(informeestadistico);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterInformeEstadisticoUpdated(informeestadistico);

            return informeestadistico;
        }

        partial void OnInformeEstadisticoDeleted(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);
        partial void OnAfterInformeEstadisticoDeleted(SgpaNew.Server.Models.Sgpa.InformeEstadistico item);

        public async Task<SgpaNew.Server.Models.Sgpa.InformeEstadistico> DeleteInformeEstadistico(int idrpt)
        {
            var itemToDelete = Context.InformeEstadisticos
                              .Where(i => i.IdRpt == idrpt)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnInformeEstadisticoDeleted(itemToDelete);


            Context.InformeEstadisticos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterInformeEstadisticoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLiquidacionBpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/liquidacionbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/liquidacionbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLiquidacionBpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/liquidacionbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/liquidacionbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLiquidacionBpsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.LiquidacionBp> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.LiquidacionBp>> GetLiquidacionBps(Query query = null)
        {
            var items = Context.LiquidacionBps.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnLiquidacionBpsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLiquidacionBpGet(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnGetLiquidacionBpByLiquidacionBpsid(ref IQueryable<SgpaNew.Server.Models.Sgpa.LiquidacionBp> items);


        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> GetLiquidacionBpByLiquidacionBpsid(int liquidacionbpsid)
        {
            var items = Context.LiquidacionBps
                              .AsNoTracking()
                              .Where(i => i.LiquidacionBPSId == liquidacionbpsid);

 
            OnGetLiquidacionBpByLiquidacionBpsid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLiquidacionBpGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLiquidacionBpCreated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnAfterLiquidacionBpCreated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);

        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> CreateLiquidacionBp(SgpaNew.Server.Models.Sgpa.LiquidacionBp liquidacionbp)
        {
            OnLiquidacionBpCreated(liquidacionbp);

            var existingItem = Context.LiquidacionBps
                              .Where(i => i.LiquidacionBPSId == liquidacionbp.LiquidacionBPSId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.LiquidacionBps.Add(liquidacionbp);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(liquidacionbp).State = EntityState.Detached;
                throw;
            }

            OnAfterLiquidacionBpCreated(liquidacionbp);

            return liquidacionbp;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> CancelLiquidacionBpChanges(SgpaNew.Server.Models.Sgpa.LiquidacionBp item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLiquidacionBpUpdated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnAfterLiquidacionBpUpdated(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);

        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> UpdateLiquidacionBp(int liquidacionbpsid, SgpaNew.Server.Models.Sgpa.LiquidacionBp liquidacionbp)
        {
            OnLiquidacionBpUpdated(liquidacionbp);

            var itemToUpdate = Context.LiquidacionBps
                              .Where(i => i.LiquidacionBPSId == liquidacionbp.LiquidacionBPSId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(liquidacionbp);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLiquidacionBpUpdated(liquidacionbp);

            return liquidacionbp;
        }

        partial void OnLiquidacionBpDeleted(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);
        partial void OnAfterLiquidacionBpDeleted(SgpaNew.Server.Models.Sgpa.LiquidacionBp item);

        public async Task<SgpaNew.Server.Models.Sgpa.LiquidacionBp> DeleteLiquidacionBp(int liquidacionbpsid)
        {
            var itemToDelete = Context.LiquidacionBps
                              .Where(i => i.LiquidacionBPSId == liquidacionbpsid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLiquidacionBpDeleted(itemToDelete);


            Context.LiquidacionBps.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLiquidacionBpDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMaeFunsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/maefuns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/maefuns/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMaeFunsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/maefuns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/maefuns/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMaeFunsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.MaeFun> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.MaeFun>> GetMaeFuns(Query query = null)
        {
            var items = Context.MaeFuns.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMaeFunsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMaeFunGet(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnGetMaeFunByNroFun(ref IQueryable<SgpaNew.Server.Models.Sgpa.MaeFun> items);


        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> GetMaeFunByNroFun(int nrofun)
        {
            var items = Context.MaeFuns
                              .AsNoTracking()
                              .Where(i => i.NroFun == nrofun);

 
            OnGetMaeFunByNroFun(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMaeFunGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMaeFunCreated(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnAfterMaeFunCreated(SgpaNew.Server.Models.Sgpa.MaeFun item);

        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> CreateMaeFun(SgpaNew.Server.Models.Sgpa.MaeFun maefun)
        {
            OnMaeFunCreated(maefun);

            var existingItem = Context.MaeFuns
                              .Where(i => i.NroFun == maefun.NroFun)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MaeFuns.Add(maefun);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(maefun).State = EntityState.Detached;
                throw;
            }

            OnAfterMaeFunCreated(maefun);

            return maefun;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> CancelMaeFunChanges(SgpaNew.Server.Models.Sgpa.MaeFun item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMaeFunUpdated(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnAfterMaeFunUpdated(SgpaNew.Server.Models.Sgpa.MaeFun item);

        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> UpdateMaeFun(int nrofun, SgpaNew.Server.Models.Sgpa.MaeFun maefun)
        {
            OnMaeFunUpdated(maefun);

            var itemToUpdate = Context.MaeFuns
                              .Where(i => i.NroFun == maefun.NroFun)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(maefun);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMaeFunUpdated(maefun);

            return maefun;
        }

        partial void OnMaeFunDeleted(SgpaNew.Server.Models.Sgpa.MaeFun item);
        partial void OnAfterMaeFunDeleted(SgpaNew.Server.Models.Sgpa.MaeFun item);

        public async Task<SgpaNew.Server.Models.Sgpa.MaeFun> DeleteMaeFun(int nrofun)
        {
            var itemToDelete = Context.MaeFuns
                              .Where(i => i.NroFun == nrofun)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMaeFunDeleted(itemToDelete);


            Context.MaeFuns.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMaeFunDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMonedaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/moneda/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/moneda/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMonedaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/moneda/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/moneda/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMonedaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Monedum> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Monedum>> GetMoneda(Query query = null)
        {
            var items = Context.Moneda.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMonedaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMonedumGet(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnGetMonedumByMoneda1(ref IQueryable<SgpaNew.Server.Models.Sgpa.Monedum> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> GetMonedumByMoneda1(string moneda1)
        {
            var items = Context.Moneda
                              .AsNoTracking()
                              .Where(i => i.Moneda1 == moneda1);

 
            OnGetMonedumByMoneda1(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMonedumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMonedumCreated(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnAfterMonedumCreated(SgpaNew.Server.Models.Sgpa.Monedum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> CreateMonedum(SgpaNew.Server.Models.Sgpa.Monedum monedum)
        {
            OnMonedumCreated(monedum);

            var existingItem = Context.Moneda
                              .Where(i => i.Moneda1 == monedum.Moneda1)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Moneda.Add(monedum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(monedum).State = EntityState.Detached;
                throw;
            }

            OnAfterMonedumCreated(monedum);

            return monedum;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> CancelMonedumChanges(SgpaNew.Server.Models.Sgpa.Monedum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMonedumUpdated(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnAfterMonedumUpdated(SgpaNew.Server.Models.Sgpa.Monedum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> UpdateMonedum(string moneda1, SgpaNew.Server.Models.Sgpa.Monedum monedum)
        {
            OnMonedumUpdated(monedum);

            var itemToUpdate = Context.Moneda
                              .Where(i => i.Moneda1 == monedum.Moneda1)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(monedum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMonedumUpdated(monedum);

            return monedum;
        }

        partial void OnMonedumDeleted(SgpaNew.Server.Models.Sgpa.Monedum item);
        partial void OnAfterMonedumDeleted(SgpaNew.Server.Models.Sgpa.Monedum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Monedum> DeleteMonedum(string moneda1)
        {
            var itemToDelete = Context.Moneda
                              .Where(i => i.Moneda1 == moneda1)
                              .Include(i => i.Prestacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMonedumDeleted(itemToDelete);


            Context.Moneda.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMonedumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMutualistaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/mutualista/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/mutualista/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMutualistaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/mutualista/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/mutualista/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMutualistaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Mutualistum> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Mutualistum>> GetMutualista(Query query = null)
        {
            var items = Context.Mutualista.AsQueryable();

            items = items.Include(i => i.FormaPago);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMutualistaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMutualistumGet(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnGetMutualistumByCodMutualista(ref IQueryable<SgpaNew.Server.Models.Sgpa.Mutualistum> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> GetMutualistumByCodMutualista(short codmutualista)
        {
            var items = Context.Mutualista
                              .AsNoTracking()
                              .Where(i => i.CodMutualista == codmutualista);

            items = items.Include(i => i.FormaPago);
 
            OnGetMutualistumByCodMutualista(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMutualistumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMutualistumCreated(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnAfterMutualistumCreated(SgpaNew.Server.Models.Sgpa.Mutualistum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> CreateMutualistum(SgpaNew.Server.Models.Sgpa.Mutualistum mutualistum)
        {
            OnMutualistumCreated(mutualistum);

            var existingItem = Context.Mutualista
                              .Where(i => i.CodMutualista == mutualistum.CodMutualista)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Mutualista.Add(mutualistum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(mutualistum).State = EntityState.Detached;
                throw;
            }

            OnAfterMutualistumCreated(mutualistum);

            return mutualistum;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> CancelMutualistumChanges(SgpaNew.Server.Models.Sgpa.Mutualistum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMutualistumUpdated(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnAfterMutualistumUpdated(SgpaNew.Server.Models.Sgpa.Mutualistum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> UpdateMutualistum(short codmutualista, SgpaNew.Server.Models.Sgpa.Mutualistum mutualistum)
        {
            OnMutualistumUpdated(mutualistum);

            var itemToUpdate = Context.Mutualista
                              .Where(i => i.CodMutualista == mutualistum.CodMutualista)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(mutualistum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMutualistumUpdated(mutualistum);

            return mutualistum;
        }

        partial void OnMutualistumDeleted(SgpaNew.Server.Models.Sgpa.Mutualistum item);
        partial void OnAfterMutualistumDeleted(SgpaNew.Server.Models.Sgpa.Mutualistum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Mutualistum> DeleteMutualistum(short codmutualista)
        {
            var itemToDelete = Context.Mutualista
                              .Where(i => i.CodMutualista == codmutualista)
                              .Include(i => i.Afiliados)
                              .Include(i => i.ReintegroMutuals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMutualistumDeleted(itemToDelete);


            Context.Mutualista.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMutualistumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportNoCargadoHlsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/nocargadohls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/nocargadohls/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNoCargadoHlsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/nocargadohls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/nocargadohls/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNoCargadoHlsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.NoCargadoHl> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.NoCargadoHl>> GetNoCargadoHls(Query query = null)
        {
            var items = Context.NoCargadoHls.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnNoCargadoHlsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNoCargadoHlGet(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnGetNoCargadoHlByNoCargadoHlid(ref IQueryable<SgpaNew.Server.Models.Sgpa.NoCargadoHl> items);


        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> GetNoCargadoHlByNoCargadoHlid(int nocargadohlid)
        {
            var items = Context.NoCargadoHls
                              .AsNoTracking()
                              .Where(i => i.NoCargadoHLId == nocargadohlid);

 
            OnGetNoCargadoHlByNoCargadoHlid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnNoCargadoHlGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnNoCargadoHlCreated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnAfterNoCargadoHlCreated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);

        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> CreateNoCargadoHl(SgpaNew.Server.Models.Sgpa.NoCargadoHl nocargadohl)
        {
            OnNoCargadoHlCreated(nocargadohl);

            var existingItem = Context.NoCargadoHls
                              .Where(i => i.NoCargadoHLId == nocargadohl.NoCargadoHLId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.NoCargadoHls.Add(nocargadohl);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(nocargadohl).State = EntityState.Detached;
                throw;
            }

            OnAfterNoCargadoHlCreated(nocargadohl);

            return nocargadohl;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> CancelNoCargadoHlChanges(SgpaNew.Server.Models.Sgpa.NoCargadoHl item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNoCargadoHlUpdated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnAfterNoCargadoHlUpdated(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);

        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> UpdateNoCargadoHl(int nocargadohlid, SgpaNew.Server.Models.Sgpa.NoCargadoHl nocargadohl)
        {
            OnNoCargadoHlUpdated(nocargadohl);

            var itemToUpdate = Context.NoCargadoHls
                              .Where(i => i.NoCargadoHLId == nocargadohl.NoCargadoHLId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(nocargadohl);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterNoCargadoHlUpdated(nocargadohl);

            return nocargadohl;
        }

        partial void OnNoCargadoHlDeleted(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);
        partial void OnAfterNoCargadoHlDeleted(SgpaNew.Server.Models.Sgpa.NoCargadoHl item);

        public async Task<SgpaNew.Server.Models.Sgpa.NoCargadoHl> DeleteNoCargadoHl(int nocargadohlid)
        {
            var itemToDelete = Context.NoCargadoHls
                              .Where(i => i.NoCargadoHLId == nocargadohlid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNoCargadoHlDeleted(itemToDelete);


            Context.NoCargadoHls.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNoCargadoHlDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportParametrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportParametrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnParametrosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Parametro> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Parametro>> GetParametros(Query query = null)
        {
            var items = Context.Parametros.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnParametrosRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportPatologiaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/patologia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/patologia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPatologiaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/patologia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/patologia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPatologiaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Patologium> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Patologium>> GetPatologia(Query query = null)
        {
            var items = Context.Patologia.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnPatologiaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPatologiumGet(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnGetPatologiumByCodPatologia(ref IQueryable<SgpaNew.Server.Models.Sgpa.Patologium> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> GetPatologiumByCodPatologia(int codpatologia)
        {
            var items = Context.Patologia
                              .AsNoTracking()
                              .Where(i => i.CodPatologia == codpatologia);

 
            OnGetPatologiumByCodPatologia(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPatologiumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPatologiumCreated(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnAfterPatologiumCreated(SgpaNew.Server.Models.Sgpa.Patologium item);

        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> CreatePatologium(SgpaNew.Server.Models.Sgpa.Patologium patologium)
        {
            OnPatologiumCreated(patologium);

            var existingItem = Context.Patologia
                              .Where(i => i.CodPatologia == patologium.CodPatologia)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Patologia.Add(patologium);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(patologium).State = EntityState.Detached;
                throw;
            }

            OnAfterPatologiumCreated(patologium);

            return patologium;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> CancelPatologiumChanges(SgpaNew.Server.Models.Sgpa.Patologium item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPatologiumUpdated(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnAfterPatologiumUpdated(SgpaNew.Server.Models.Sgpa.Patologium item);

        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> UpdatePatologium(int codpatologia, SgpaNew.Server.Models.Sgpa.Patologium patologium)
        {
            OnPatologiumUpdated(patologium);

            var itemToUpdate = Context.Patologia
                              .Where(i => i.CodPatologia == patologium.CodPatologia)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(patologium);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPatologiumUpdated(patologium);

            return patologium;
        }

        partial void OnPatologiumDeleted(SgpaNew.Server.Models.Sgpa.Patologium item);
        partial void OnAfterPatologiumDeleted(SgpaNew.Server.Models.Sgpa.Patologium item);

        public async Task<SgpaNew.Server.Models.Sgpa.Patologium> DeletePatologium(int codpatologia)
        {
            var itemToDelete = Context.Patologia
                              .Where(i => i.CodPatologia == codpatologia)
                              .Include(i => i.AfeccionGrupos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPatologiumDeleted(itemToDelete);


            Context.Patologia.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPatologiumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportPrestacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPrestacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPrestacionsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Prestacion> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Prestacion>> GetPrestacions(Query query = null)
        {
            var items = Context.Prestacions.AsQueryable();

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.PrestacionTipo);
            items = items.Include(i => i.Monedum);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnPrestacionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPrestacionGet(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnGetPrestacionByPrestacionId(ref IQueryable<SgpaNew.Server.Models.Sgpa.Prestacion> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> GetPrestacionByPrestacionId(int prestacionid)
        {
            var items = Context.Prestacions
                              .AsNoTracking()
                              .Where(i => i.PrestacionId == prestacionid);

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.PrestacionTipo);
            items = items.Include(i => i.Monedum);
 
            OnGetPrestacionByPrestacionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPrestacionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPrestacionCreated(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnAfterPrestacionCreated(SgpaNew.Server.Models.Sgpa.Prestacion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> CreatePrestacion(SgpaNew.Server.Models.Sgpa.Prestacion prestacion)
        {
            OnPrestacionCreated(prestacion);

            var existingItem = Context.Prestacions
                              .Where(i => i.PrestacionId == prestacion.PrestacionId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Prestacions.Add(prestacion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(prestacion).State = EntityState.Detached;
                throw;
            }

            OnAfterPrestacionCreated(prestacion);

            return prestacion;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> CancelPrestacionChanges(SgpaNew.Server.Models.Sgpa.Prestacion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPrestacionUpdated(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnAfterPrestacionUpdated(SgpaNew.Server.Models.Sgpa.Prestacion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> UpdatePrestacion(int prestacionid, SgpaNew.Server.Models.Sgpa.Prestacion prestacion)
        {
            OnPrestacionUpdated(prestacion);

            var itemToUpdate = Context.Prestacions
                              .Where(i => i.PrestacionId == prestacion.PrestacionId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(prestacion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPrestacionUpdated(prestacion);

            return prestacion;
        }

        partial void OnPrestacionDeleted(SgpaNew.Server.Models.Sgpa.Prestacion item);
        partial void OnAfterPrestacionDeleted(SgpaNew.Server.Models.Sgpa.Prestacion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Prestacion> DeletePrestacion(int prestacionid)
        {
            var itemToDelete = Context.Prestacions
                              .Where(i => i.PrestacionId == prestacionid)
                              .Include(i => i.Receta)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPrestacionDeleted(itemToDelete);


            Context.Prestacions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPrestacionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportPrestacionTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestaciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestaciontipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPrestacionTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/prestaciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/prestaciontipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPrestacionTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.PrestacionTipo> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.PrestacionTipo>> GetPrestacionTipos(Query query = null)
        {
            var items = Context.PrestacionTipos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnPrestacionTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPrestacionTipoGet(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnGetPrestacionTipoByCodPrestacionTipo(ref IQueryable<SgpaNew.Server.Models.Sgpa.PrestacionTipo> items);


        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> GetPrestacionTipoByCodPrestacionTipo(short codprestaciontipo)
        {
            var items = Context.PrestacionTipos
                              .AsNoTracking()
                              .Where(i => i.CodPrestacionTipo == codprestaciontipo);

 
            OnGetPrestacionTipoByCodPrestacionTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPrestacionTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPrestacionTipoCreated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnAfterPrestacionTipoCreated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> CreatePrestacionTipo(SgpaNew.Server.Models.Sgpa.PrestacionTipo prestaciontipo)
        {
            OnPrestacionTipoCreated(prestaciontipo);

            var existingItem = Context.PrestacionTipos
                              .Where(i => i.CodPrestacionTipo == prestaciontipo.CodPrestacionTipo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.PrestacionTipos.Add(prestaciontipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(prestaciontipo).State = EntityState.Detached;
                throw;
            }

            OnAfterPrestacionTipoCreated(prestaciontipo);

            return prestaciontipo;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> CancelPrestacionTipoChanges(SgpaNew.Server.Models.Sgpa.PrestacionTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPrestacionTipoUpdated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnAfterPrestacionTipoUpdated(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> UpdatePrestacionTipo(short codprestaciontipo, SgpaNew.Server.Models.Sgpa.PrestacionTipo prestaciontipo)
        {
            OnPrestacionTipoUpdated(prestaciontipo);

            var itemToUpdate = Context.PrestacionTipos
                              .Where(i => i.CodPrestacionTipo == prestaciontipo.CodPrestacionTipo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(prestaciontipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPrestacionTipoUpdated(prestaciontipo);

            return prestaciontipo;
        }

        partial void OnPrestacionTipoDeleted(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);
        partial void OnAfterPrestacionTipoDeleted(SgpaNew.Server.Models.Sgpa.PrestacionTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.PrestacionTipo> DeletePrestacionTipo(short codprestaciontipo)
        {
            var itemToDelete = Context.PrestacionTipos
                              .Where(i => i.CodPrestacionTipo == codprestaciontipo)
                              .Include(i => i.Prestacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPrestacionTipoDeleted(itemToDelete);


            Context.PrestacionTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPrestacionTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportPrimaFallecimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/primafallecimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/primafallecimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPrimaFallecimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/primafallecimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/primafallecimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPrimaFallecimientosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento>> GetPrimaFallecimientos(Query query = null)
        {
            var items = Context.PrimaFallecimientos.AsQueryable();

            items = items.Include(i => i.Afiliado);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnPrimaFallecimientosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPrimaFallecimientoGet(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnGetPrimaFallecimientoByCi(ref IQueryable<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> items);


        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> GetPrimaFallecimientoByCi(int ci)
        {
            var items = Context.PrimaFallecimientos
                              .AsNoTracking()
                              .Where(i => i.CI == ci);

            items = items.Include(i => i.Afiliado);
 
            OnGetPrimaFallecimientoByCi(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPrimaFallecimientoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPrimaFallecimientoCreated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnAfterPrimaFallecimientoCreated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);

        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> CreatePrimaFallecimiento(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento primafallecimiento)
        {
            OnPrimaFallecimientoCreated(primafallecimiento);

            var existingItem = Context.PrimaFallecimientos
                              .Where(i => i.CI == primafallecimiento.CI)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.PrimaFallecimientos.Add(primafallecimiento);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(primafallecimiento).State = EntityState.Detached;
                throw;
            }

            OnAfterPrimaFallecimientoCreated(primafallecimiento);

            return primafallecimiento;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> CancelPrimaFallecimientoChanges(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPrimaFallecimientoUpdated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnAfterPrimaFallecimientoUpdated(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);

        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> UpdatePrimaFallecimiento(int ci, SgpaNew.Server.Models.Sgpa.PrimaFallecimiento primafallecimiento)
        {
            OnPrimaFallecimientoUpdated(primafallecimiento);

            var itemToUpdate = Context.PrimaFallecimientos
                              .Where(i => i.CI == primafallecimiento.CI)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(primafallecimiento);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPrimaFallecimientoUpdated(primafallecimiento);

            return primafallecimiento;
        }

        partial void OnPrimaFallecimientoDeleted(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);
        partial void OnAfterPrimaFallecimientoDeleted(SgpaNew.Server.Models.Sgpa.PrimaFallecimiento item);

        public async Task<SgpaNew.Server.Models.Sgpa.PrimaFallecimiento> DeletePrimaFallecimiento(int ci)
        {
            var itemToDelete = Context.PrimaFallecimientos
                              .Where(i => i.CI == ci)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPrimaFallecimientoDeleted(itemToDelete);


            Context.PrimaFallecimientos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPrimaFallecimientoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRecetaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/receta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/receta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRecetaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/receta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/receta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRecetaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Recetum> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Recetum>> GetReceta(Query query = null)
        {
            var items = Context.Receta.AsQueryable();

            items = items.Include(i => i.RecetaDistancium);
            items = items.Include(i => i.Prestacion);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRecetaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRecetumGet(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnGetRecetumByRecetaId(ref IQueryable<SgpaNew.Server.Models.Sgpa.Recetum> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> GetRecetumByRecetaId(int recetaid)
        {
            var items = Context.Receta
                              .AsNoTracking()
                              .Where(i => i.RecetaId == recetaid);

            items = items.Include(i => i.RecetaDistancium);
            items = items.Include(i => i.Prestacion);
 
            OnGetRecetumByRecetaId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRecetumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRecetumCreated(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnAfterRecetumCreated(SgpaNew.Server.Models.Sgpa.Recetum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> CreateRecetum(SgpaNew.Server.Models.Sgpa.Recetum recetum)
        {
            OnRecetumCreated(recetum);

            var existingItem = Context.Receta
                              .Where(i => i.RecetaId == recetum.RecetaId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Receta.Add(recetum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(recetum).State = EntityState.Detached;
                throw;
            }

            OnAfterRecetumCreated(recetum);

            return recetum;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> CancelRecetumChanges(SgpaNew.Server.Models.Sgpa.Recetum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRecetumUpdated(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnAfterRecetumUpdated(SgpaNew.Server.Models.Sgpa.Recetum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> UpdateRecetum(int recetaid, SgpaNew.Server.Models.Sgpa.Recetum recetum)
        {
            OnRecetumUpdated(recetum);

            var itemToUpdate = Context.Receta
                              .Where(i => i.RecetaId == recetum.RecetaId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(recetum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRecetumUpdated(recetum);

            return recetum;
        }

        partial void OnRecetumDeleted(SgpaNew.Server.Models.Sgpa.Recetum item);
        partial void OnAfterRecetumDeleted(SgpaNew.Server.Models.Sgpa.Recetum item);

        public async Task<SgpaNew.Server.Models.Sgpa.Recetum> DeleteRecetum(int recetaid)
        {
            var itemToDelete = Context.Receta
                              .Where(i => i.RecetaId == recetaid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRecetumDeleted(itemToDelete);


            Context.Receta.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRecetumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRecetaDistanciaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/recetadistancia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/recetadistancia/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRecetaDistanciaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/recetadistancia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/recetadistancia/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRecetaDistanciaRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.RecetaDistancium> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.RecetaDistancium>> GetRecetaDistancia(Query query = null)
        {
            var items = Context.RecetaDistancia.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRecetaDistanciaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRecetaDistanciumGet(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnGetRecetaDistanciumByCodRecetaDistancia(ref IQueryable<SgpaNew.Server.Models.Sgpa.RecetaDistancium> items);


        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> GetRecetaDistanciumByCodRecetaDistancia(string codrecetadistancia)
        {
            var items = Context.RecetaDistancia
                              .AsNoTracking()
                              .Where(i => i.CodRecetaDistancia == codrecetadistancia);

 
            OnGetRecetaDistanciumByCodRecetaDistancia(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRecetaDistanciumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRecetaDistanciumCreated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnAfterRecetaDistanciumCreated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);

        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> CreateRecetaDistancium(SgpaNew.Server.Models.Sgpa.RecetaDistancium recetadistancium)
        {
            OnRecetaDistanciumCreated(recetadistancium);

            var existingItem = Context.RecetaDistancia
                              .Where(i => i.CodRecetaDistancia == recetadistancium.CodRecetaDistancia)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RecetaDistancia.Add(recetadistancium);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(recetadistancium).State = EntityState.Detached;
                throw;
            }

            OnAfterRecetaDistanciumCreated(recetadistancium);

            return recetadistancium;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> CancelRecetaDistanciumChanges(SgpaNew.Server.Models.Sgpa.RecetaDistancium item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRecetaDistanciumUpdated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnAfterRecetaDistanciumUpdated(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);

        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> UpdateRecetaDistancium(string codrecetadistancia, SgpaNew.Server.Models.Sgpa.RecetaDistancium recetadistancium)
        {
            OnRecetaDistanciumUpdated(recetadistancium);

            var itemToUpdate = Context.RecetaDistancia
                              .Where(i => i.CodRecetaDistancia == recetadistancium.CodRecetaDistancia)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(recetadistancium);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRecetaDistanciumUpdated(recetadistancium);

            return recetadistancium;
        }

        partial void OnRecetaDistanciumDeleted(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);
        partial void OnAfterRecetaDistanciumDeleted(SgpaNew.Server.Models.Sgpa.RecetaDistancium item);

        public async Task<SgpaNew.Server.Models.Sgpa.RecetaDistancium> DeleteRecetaDistancium(string codrecetadistancia)
        {
            var itemToDelete = Context.RecetaDistancia
                              .Where(i => i.CodRecetaDistancia == codrecetadistancia)
                              .Include(i => i.Receta)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRecetaDistanciumDeleted(itemToDelete);


            Context.RecetaDistancia.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRecetaDistanciumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegimenAportesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenaportes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenaportes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegimenAportesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenaportes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenaportes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegimenAportesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.RegimenAporte> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.RegimenAporte>> GetRegimenAportes(Query query = null)
        {
            var items = Context.RegimenAportes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRegimenAportesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegimenAporteGet(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnGetRegimenAporteByCodRegimenAporte(ref IQueryable<SgpaNew.Server.Models.Sgpa.RegimenAporte> items);


        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> GetRegimenAporteByCodRegimenAporte(short codregimenaporte)
        {
            var items = Context.RegimenAportes
                              .AsNoTracking()
                              .Where(i => i.CodRegimenAporte == codregimenaporte);

 
            OnGetRegimenAporteByCodRegimenAporte(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegimenAporteGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegimenAporteCreated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnAfterRegimenAporteCreated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> CreateRegimenAporte(SgpaNew.Server.Models.Sgpa.RegimenAporte regimenaporte)
        {
            OnRegimenAporteCreated(regimenaporte);

            var existingItem = Context.RegimenAportes
                              .Where(i => i.CodRegimenAporte == regimenaporte.CodRegimenAporte)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RegimenAportes.Add(regimenaporte);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(regimenaporte).State = EntityState.Detached;
                throw;
            }

            OnAfterRegimenAporteCreated(regimenaporte);

            return regimenaporte;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> CancelRegimenAporteChanges(SgpaNew.Server.Models.Sgpa.RegimenAporte item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegimenAporteUpdated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnAfterRegimenAporteUpdated(SgpaNew.Server.Models.Sgpa.RegimenAporte item);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> UpdateRegimenAporte(short codregimenaporte, SgpaNew.Server.Models.Sgpa.RegimenAporte regimenaporte)
        {
            OnRegimenAporteUpdated(regimenaporte);

            var itemToUpdate = Context.RegimenAportes
                              .Where(i => i.CodRegimenAporte == regimenaporte.CodRegimenAporte)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(regimenaporte);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegimenAporteUpdated(regimenaporte);

            return regimenaporte;
        }

        partial void OnRegimenAporteDeleted(SgpaNew.Server.Models.Sgpa.RegimenAporte item);
        partial void OnAfterRegimenAporteDeleted(SgpaNew.Server.Models.Sgpa.RegimenAporte item);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenAporte> DeleteRegimenAporte(short codregimenaporte)
        {
            var itemToDelete = Context.RegimenAportes
                              .Where(i => i.CodRegimenAporte == codregimenaporte)
                              .Include(i => i.Empresas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegimenAporteDeleted(itemToDelete);


            Context.RegimenAportes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegimenAporteDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegimenJubilatoriosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenjubilatorios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenjubilatorios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegimenJubilatoriosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/regimenjubilatorios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/regimenjubilatorios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegimenJubilatoriosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio>> GetRegimenJubilatorios(Query query = null)
        {
            var items = Context.RegimenJubilatorios.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnRegimenJubilatoriosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegimenJubilatorioGet(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnGetRegimenJubilatorioByCodRegimenJubilatorio(ref IQueryable<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> items);


        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> GetRegimenJubilatorioByCodRegimenJubilatorio(byte codregimenjubilatorio)
        {
            var items = Context.RegimenJubilatorios
                              .AsNoTracking()
                              .Where(i => i.CodRegimenJubilatorio == codregimenjubilatorio);

 
            OnGetRegimenJubilatorioByCodRegimenJubilatorio(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegimenJubilatorioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegimenJubilatorioCreated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnAfterRegimenJubilatorioCreated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> CreateRegimenJubilatorio(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio regimenjubilatorio)
        {
            OnRegimenJubilatorioCreated(regimenjubilatorio);

            var existingItem = Context.RegimenJubilatorios
                              .Where(i => i.CodRegimenJubilatorio == regimenjubilatorio.CodRegimenJubilatorio)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RegimenJubilatorios.Add(regimenjubilatorio);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(regimenjubilatorio).State = EntityState.Detached;
                throw;
            }

            OnAfterRegimenJubilatorioCreated(regimenjubilatorio);

            return regimenjubilatorio;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> CancelRegimenJubilatorioChanges(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegimenJubilatorioUpdated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnAfterRegimenJubilatorioUpdated(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> UpdateRegimenJubilatorio(byte codregimenjubilatorio, SgpaNew.Server.Models.Sgpa.RegimenJubilatorio regimenjubilatorio)
        {
            OnRegimenJubilatorioUpdated(regimenjubilatorio);

            var itemToUpdate = Context.RegimenJubilatorios
                              .Where(i => i.CodRegimenJubilatorio == regimenjubilatorio.CodRegimenJubilatorio)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(regimenjubilatorio);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegimenJubilatorioUpdated(regimenjubilatorio);

            return regimenjubilatorio;
        }

        partial void OnRegimenJubilatorioDeleted(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);
        partial void OnAfterRegimenJubilatorioDeleted(SgpaNew.Server.Models.Sgpa.RegimenJubilatorio item);

        public async Task<SgpaNew.Server.Models.Sgpa.RegimenJubilatorio> DeleteRegimenJubilatorio(byte codregimenjubilatorio)
        {
            var itemToDelete = Context.RegimenJubilatorios
                              .Where(i => i.CodRegimenJubilatorio == codregimenjubilatorio)
                              .Include(i => i.Afiliados)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegimenJubilatorioDeleted(itemToDelete);


            Context.RegimenJubilatorios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegimenJubilatorioDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportReintegroMutualsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/reintegromutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/reintegromutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportReintegroMutualsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/reintegromutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/reintegromutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnReintegroMutualsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.ReintegroMutual> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.ReintegroMutual>> GetReintegroMutuals(Query query = null)
        {
            var items = Context.ReintegroMutuals.AsQueryable();

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.Mutualistum);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnReintegroMutualsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnReintegroMutualGet(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnGetReintegroMutualByReintegroMutualId(ref IQueryable<SgpaNew.Server.Models.Sgpa.ReintegroMutual> items);


        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> GetReintegroMutualByReintegroMutualId(int reintegromutualid)
        {
            var items = Context.ReintegroMutuals
                              .AsNoTracking()
                              .Where(i => i.ReintegroMutualId == reintegromutualid);

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.Mutualistum);
 
            OnGetReintegroMutualByReintegroMutualId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnReintegroMutualGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnReintegroMutualCreated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnAfterReintegroMutualCreated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);

        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> CreateReintegroMutual(SgpaNew.Server.Models.Sgpa.ReintegroMutual reintegromutual)
        {
            OnReintegroMutualCreated(reintegromutual);

            var existingItem = Context.ReintegroMutuals
                              .Where(i => i.ReintegroMutualId == reintegromutual.ReintegroMutualId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ReintegroMutuals.Add(reintegromutual);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(reintegromutual).State = EntityState.Detached;
                throw;
            }

            OnAfterReintegroMutualCreated(reintegromutual);

            return reintegromutual;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> CancelReintegroMutualChanges(SgpaNew.Server.Models.Sgpa.ReintegroMutual item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnReintegroMutualUpdated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnAfterReintegroMutualUpdated(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);

        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> UpdateReintegroMutual(int reintegromutualid, SgpaNew.Server.Models.Sgpa.ReintegroMutual reintegromutual)
        {
            OnReintegroMutualUpdated(reintegromutual);

            var itemToUpdate = Context.ReintegroMutuals
                              .Where(i => i.ReintegroMutualId == reintegromutual.ReintegroMutualId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(reintegromutual);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterReintegroMutualUpdated(reintegromutual);

            return reintegromutual;
        }

        partial void OnReintegroMutualDeleted(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);
        partial void OnAfterReintegroMutualDeleted(SgpaNew.Server.Models.Sgpa.ReintegroMutual item);

        public async Task<SgpaNew.Server.Models.Sgpa.ReintegroMutual> DeleteReintegroMutual(int reintegromutualid)
        {
            var itemToDelete = Context.ReintegroMutuals
                              .Where(i => i.ReintegroMutualId == reintegromutualid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnReintegroMutualDeleted(itemToDelete);


            Context.ReintegroMutuals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterReintegroMutualDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalidaTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/salidatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/salidatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalidaTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/salidatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/salidatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalidaTiposRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SalidaTipo> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SalidaTipo>> GetSalidaTipos(Query query = null)
        {
            var items = Context.SalidaTipos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSalidaTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalidaTipoGet(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnGetSalidaTipoByCodSalidaTipo(ref IQueryable<SgpaNew.Server.Models.Sgpa.SalidaTipo> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> GetSalidaTipoByCodSalidaTipo(short codsalidatipo)
        {
            var items = Context.SalidaTipos
                              .AsNoTracking()
                              .Where(i => i.CodSalidaTipo == codsalidatipo);

 
            OnGetSalidaTipoByCodSalidaTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalidaTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalidaTipoCreated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnAfterSalidaTipoCreated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> CreateSalidaTipo(SgpaNew.Server.Models.Sgpa.SalidaTipo salidatipo)
        {
            OnSalidaTipoCreated(salidatipo);

            var existingItem = Context.SalidaTipos
                              .Where(i => i.CodSalidaTipo == salidatipo.CodSalidaTipo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalidaTipos.Add(salidatipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salidatipo).State = EntityState.Detached;
                throw;
            }

            OnAfterSalidaTipoCreated(salidatipo);

            return salidatipo;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> CancelSalidaTipoChanges(SgpaNew.Server.Models.Sgpa.SalidaTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalidaTipoUpdated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnAfterSalidaTipoUpdated(SgpaNew.Server.Models.Sgpa.SalidaTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> UpdateSalidaTipo(short codsalidatipo, SgpaNew.Server.Models.Sgpa.SalidaTipo salidatipo)
        {
            OnSalidaTipoUpdated(salidatipo);

            var itemToUpdate = Context.SalidaTipos
                              .Where(i => i.CodSalidaTipo == salidatipo.CodSalidaTipo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salidatipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalidaTipoUpdated(salidatipo);

            return salidatipo;
        }

        partial void OnSalidaTipoDeleted(SgpaNew.Server.Models.Sgpa.SalidaTipo item);
        partial void OnAfterSalidaTipoDeleted(SgpaNew.Server.Models.Sgpa.SalidaTipo item);

        public async Task<SgpaNew.Server.Models.Sgpa.SalidaTipo> DeleteSalidaTipo(short codsalidatipo)
        {
            var itemToDelete = Context.SalidaTipos
                              .Where(i => i.CodSalidaTipo == codsalidatipo)
                              .Include(i => i.Certificacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalidaTipoDeleted(itemToDelete);


            Context.SalidaTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalidaTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSeleccionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/seleccions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/seleccions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSeleccionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/seleccions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/seleccions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSeleccionsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Seleccion> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Seleccion>> GetSeleccions(Query query = null)
        {
            var items = Context.Seleccions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSeleccionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSeleccionGet(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnGetSeleccionBySeleccionId(ref IQueryable<SgpaNew.Server.Models.Sgpa.Seleccion> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> GetSeleccionBySeleccionId(int seleccionid)
        {
            var items = Context.Seleccions
                              .AsNoTracking()
                              .Where(i => i.SeleccionId == seleccionid);

 
            OnGetSeleccionBySeleccionId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSeleccionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSeleccionCreated(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnAfterSeleccionCreated(SgpaNew.Server.Models.Sgpa.Seleccion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> CreateSeleccion(SgpaNew.Server.Models.Sgpa.Seleccion seleccion)
        {
            OnSeleccionCreated(seleccion);

            var existingItem = Context.Seleccions
                              .Where(i => i.SeleccionId == seleccion.SeleccionId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Seleccions.Add(seleccion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(seleccion).State = EntityState.Detached;
                throw;
            }

            OnAfterSeleccionCreated(seleccion);

            return seleccion;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> CancelSeleccionChanges(SgpaNew.Server.Models.Sgpa.Seleccion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSeleccionUpdated(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnAfterSeleccionUpdated(SgpaNew.Server.Models.Sgpa.Seleccion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> UpdateSeleccion(int seleccionid, SgpaNew.Server.Models.Sgpa.Seleccion seleccion)
        {
            OnSeleccionUpdated(seleccion);

            var itemToUpdate = Context.Seleccions
                              .Where(i => i.SeleccionId == seleccion.SeleccionId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(seleccion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSeleccionUpdated(seleccion);

            return seleccion;
        }

        partial void OnSeleccionDeleted(SgpaNew.Server.Models.Sgpa.Seleccion item);
        partial void OnAfterSeleccionDeleted(SgpaNew.Server.Models.Sgpa.Seleccion item);

        public async Task<SgpaNew.Server.Models.Sgpa.Seleccion> DeleteSeleccion(int seleccionid)
        {
            var itemToDelete = Context.Seleccions
                              .Where(i => i.SeleccionId == seleccionid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSeleccionDeleted(itemToDelete);


            Context.Seleccions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSeleccionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSituacionMutualsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionmutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionmutuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSituacionMutualsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionmutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionmutuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSituacionMutualsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SituacionMutual> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SituacionMutual>> GetSituacionMutuals(Query query = null)
        {
            var items = Context.SituacionMutuals.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSituacionMutualsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSituacionMutualGet(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnGetSituacionMutualByCodSituacionMutual(ref IQueryable<SgpaNew.Server.Models.Sgpa.SituacionMutual> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> GetSituacionMutualByCodSituacionMutual(string codsituacionmutual)
        {
            var items = Context.SituacionMutuals
                              .AsNoTracking()
                              .Where(i => i.CodSituacionMutual == codsituacionmutual);

 
            OnGetSituacionMutualByCodSituacionMutual(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSituacionMutualGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSituacionMutualCreated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnAfterSituacionMutualCreated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> CreateSituacionMutual(SgpaNew.Server.Models.Sgpa.SituacionMutual situacionmutual)
        {
            OnSituacionMutualCreated(situacionmutual);

            var existingItem = Context.SituacionMutuals
                              .Where(i => i.CodSituacionMutual == situacionmutual.CodSituacionMutual)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SituacionMutuals.Add(situacionmutual);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(situacionmutual).State = EntityState.Detached;
                throw;
            }

            OnAfterSituacionMutualCreated(situacionmutual);

            return situacionmutual;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> CancelSituacionMutualChanges(SgpaNew.Server.Models.Sgpa.SituacionMutual item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSituacionMutualUpdated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnAfterSituacionMutualUpdated(SgpaNew.Server.Models.Sgpa.SituacionMutual item);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> UpdateSituacionMutual(string codsituacionmutual, SgpaNew.Server.Models.Sgpa.SituacionMutual situacionmutual)
        {
            OnSituacionMutualUpdated(situacionmutual);

            var itemToUpdate = Context.SituacionMutuals
                              .Where(i => i.CodSituacionMutual == situacionmutual.CodSituacionMutual)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(situacionmutual);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSituacionMutualUpdated(situacionmutual);

            return situacionmutual;
        }

        partial void OnSituacionMutualDeleted(SgpaNew.Server.Models.Sgpa.SituacionMutual item);
        partial void OnAfterSituacionMutualDeleted(SgpaNew.Server.Models.Sgpa.SituacionMutual item);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionMutual> DeleteSituacionMutual(string codsituacionmutual)
        {
            var itemToDelete = Context.SituacionMutuals
                              .Where(i => i.CodSituacionMutual == codsituacionmutual)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSituacionMutualDeleted(itemToDelete);


            Context.SituacionMutuals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSituacionMutualDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSituacionPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSituacionPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/situacionpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/situacionpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSituacionPagosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SituacionPago> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SituacionPago>> GetSituacionPagos(Query query = null)
        {
            var items = Context.SituacionPagos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSituacionPagosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSituacionPagoGet(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnGetSituacionPagoByCodSituacionPago(ref IQueryable<SgpaNew.Server.Models.Sgpa.SituacionPago> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> GetSituacionPagoByCodSituacionPago(short codsituacionpago)
        {
            var items = Context.SituacionPagos
                              .AsNoTracking()
                              .Where(i => i.CodSituacionPago == codsituacionpago);

 
            OnGetSituacionPagoByCodSituacionPago(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSituacionPagoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSituacionPagoCreated(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnAfterSituacionPagoCreated(SgpaNew.Server.Models.Sgpa.SituacionPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> CreateSituacionPago(SgpaNew.Server.Models.Sgpa.SituacionPago situacionpago)
        {
            OnSituacionPagoCreated(situacionpago);

            var existingItem = Context.SituacionPagos
                              .Where(i => i.CodSituacionPago == situacionpago.CodSituacionPago)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SituacionPagos.Add(situacionpago);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(situacionpago).State = EntityState.Detached;
                throw;
            }

            OnAfterSituacionPagoCreated(situacionpago);

            return situacionpago;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> CancelSituacionPagoChanges(SgpaNew.Server.Models.Sgpa.SituacionPago item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSituacionPagoUpdated(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnAfterSituacionPagoUpdated(SgpaNew.Server.Models.Sgpa.SituacionPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> UpdateSituacionPago(short codsituacionpago, SgpaNew.Server.Models.Sgpa.SituacionPago situacionpago)
        {
            OnSituacionPagoUpdated(situacionpago);

            var itemToUpdate = Context.SituacionPagos
                              .Where(i => i.CodSituacionPago == situacionpago.CodSituacionPago)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(situacionpago);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSituacionPagoUpdated(situacionpago);

            return situacionpago;
        }

        partial void OnSituacionPagoDeleted(SgpaNew.Server.Models.Sgpa.SituacionPago item);
        partial void OnAfterSituacionPagoDeleted(SgpaNew.Server.Models.Sgpa.SituacionPago item);

        public async Task<SgpaNew.Server.Models.Sgpa.SituacionPago> DeleteSituacionPago(short codsituacionpago)
        {
            var itemToDelete = Context.SituacionPagos
                              .Where(i => i.CodSituacionPago == codsituacionpago)
                              .Include(i => i.Empresas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSituacionPagoDeleted(itemToDelete);


            Context.SituacionPagos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSituacionPagoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioCabezalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioCabezalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioCabezalsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal>> GetSubsidioCabezals(Query query = null)
        {
            var items = Context.SubsidioCabezals.AsQueryable();

            items = items.Include(i => i.Afiliado);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioCabezalsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioCabezalGet(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnGetSubsidioCabezalByIdSubsidio(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> GetSubsidioCabezalByIdSubsidio(int idsubsidio)
        {
            var items = Context.SubsidioCabezals
                              .AsNoTracking()
                              .Where(i => i.IdSubsidio == idsubsidio);

            items = items.Include(i => i.Afiliado);
 
            OnGetSubsidioCabezalByIdSubsidio(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioCabezalGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioCabezalCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnAfterSubsidioCabezalCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> CreateSubsidioCabezal(SgpaNew.Server.Models.Sgpa.SubsidioCabezal subsidiocabezal)
        {
            OnSubsidioCabezalCreated(subsidiocabezal);

            var existingItem = Context.SubsidioCabezals
                              .Where(i => i.IdSubsidio == subsidiocabezal.IdSubsidio)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioCabezals.Add(subsidiocabezal);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidiocabezal).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioCabezalCreated(subsidiocabezal);

            return subsidiocabezal;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> CancelSubsidioCabezalChanges(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioCabezalUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnAfterSubsidioCabezalUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> UpdateSubsidioCabezal(int idsubsidio, SgpaNew.Server.Models.Sgpa.SubsidioCabezal subsidiocabezal)
        {
            OnSubsidioCabezalUpdated(subsidiocabezal);

            var itemToUpdate = Context.SubsidioCabezals
                              .Where(i => i.IdSubsidio == subsidiocabezal.IdSubsidio)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidiocabezal);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioCabezalUpdated(subsidiocabezal);

            return subsidiocabezal;
        }

        partial void OnSubsidioCabezalDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);
        partial void OnAfterSubsidioCabezalDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezal item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezal> DeleteSubsidioCabezal(int idsubsidio)
        {
            var itemToDelete = Context.SubsidioCabezals
                              .Where(i => i.IdSubsidio == idsubsidio)
                              .Include(i => i.SubsidiocabezalBps)
                              .Include(i => i.SubsidioCabezalEmpresas)
                              .Include(i => i.SubsidioEnfermedads)
                              .Include(i => i.SubsidioItems)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioCabezalDeleted(itemToDelete);


            Context.SubsidioCabezals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioCabezalDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidiocabezalBpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalbps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidiocabezalBpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalbps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidiocabezalBpsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp>> GetSubsidiocabezalBps(Query query = null)
        {
            var items = Context.SubsidiocabezalBps.AsQueryable();

            items = items.Include(i => i.SubsidioCabezal);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidiocabezalBpsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidiocabezalBpGet(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnGetSubsidiocabezalBpByIdSubsidio(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> GetSubsidiocabezalBpByIdSubsidio(int idsubsidio)
        {
            var items = Context.SubsidiocabezalBps
                              .AsNoTracking()
                              .Where(i => i.IdSubsidio == idsubsidio);

            items = items.Include(i => i.SubsidioCabezal);
 
            OnGetSubsidiocabezalBpByIdSubsidio(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidiocabezalBpGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidiocabezalBpCreated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnAfterSubsidiocabezalBpCreated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> CreateSubsidiocabezalBp(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp subsidiocabezalbp)
        {
            OnSubsidiocabezalBpCreated(subsidiocabezalbp);

            var existingItem = Context.SubsidiocabezalBps
                              .Where(i => i.IdSubsidio == subsidiocabezalbp.IdSubsidio)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidiocabezalBps.Add(subsidiocabezalbp);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidiocabezalbp).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidiocabezalBpCreated(subsidiocabezalbp);

            return subsidiocabezalbp;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> CancelSubsidiocabezalBpChanges(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidiocabezalBpUpdated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnAfterSubsidiocabezalBpUpdated(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> UpdateSubsidiocabezalBp(int idsubsidio, SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp subsidiocabezalbp)
        {
            OnSubsidiocabezalBpUpdated(subsidiocabezalbp);

            var itemToUpdate = Context.SubsidiocabezalBps
                              .Where(i => i.IdSubsidio == subsidiocabezalbp.IdSubsidio)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidiocabezalbp);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidiocabezalBpUpdated(subsidiocabezalbp);

            return subsidiocabezalbp;
        }

        partial void OnSubsidiocabezalBpDeleted(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);
        partial void OnAfterSubsidiocabezalBpDeleted(SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidiocabezalBp> DeleteSubsidiocabezalBp(int idsubsidio)
        {
            var itemToDelete = Context.SubsidiocabezalBps
                              .Where(i => i.IdSubsidio == idsubsidio)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidiocabezalBpDeleted(itemToDelete);


            Context.SubsidiocabezalBps.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidiocabezalBpDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioCabezalEmpresasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioCabezalEmpresasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidiocabezalempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidiocabezalempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioCabezalEmpresasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa>> GetSubsidioCabezalEmpresas(Query query = null)
        {
            var items = Context.SubsidioCabezalEmpresas.AsQueryable();

            items = items.Include(i => i.Empresa);
            items = items.Include(i => i.SubsidioCabezal);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioCabezalEmpresasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioCabezalEmpresaGet(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnGetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> GetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(int subsidiocabezalempresaid)
        {
            var items = Context.SubsidioCabezalEmpresas
                              .AsNoTracking()
                              .Where(i => i.SubsidioCabezalempresaId == subsidiocabezalempresaid);

            items = items.Include(i => i.Empresa);
            items = items.Include(i => i.SubsidioCabezal);
 
            OnGetSubsidioCabezalEmpresaBySubsidioCabezalempresaId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioCabezalEmpresaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioCabezalEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnAfterSubsidioCabezalEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> CreateSubsidioCabezalEmpresa(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa subsidiocabezalempresa)
        {
            OnSubsidioCabezalEmpresaCreated(subsidiocabezalempresa);

            var existingItem = Context.SubsidioCabezalEmpresas
                              .Where(i => i.SubsidioCabezalempresaId == subsidiocabezalempresa.SubsidioCabezalempresaId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioCabezalEmpresas.Add(subsidiocabezalempresa);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidiocabezalempresa).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioCabezalEmpresaCreated(subsidiocabezalempresa);

            return subsidiocabezalempresa;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> CancelSubsidioCabezalEmpresaChanges(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioCabezalEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnAfterSubsidioCabezalEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> UpdateSubsidioCabezalEmpresa(int subsidiocabezalempresaid, SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa subsidiocabezalempresa)
        {
            OnSubsidioCabezalEmpresaUpdated(subsidiocabezalempresa);

            var itemToUpdate = Context.SubsidioCabezalEmpresas
                              .Where(i => i.SubsidioCabezalempresaId == subsidiocabezalempresa.SubsidioCabezalempresaId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidiocabezalempresa);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioCabezalEmpresaUpdated(subsidiocabezalempresa);

            return subsidiocabezalempresa;
        }

        partial void OnSubsidioCabezalEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);
        partial void OnAfterSubsidioCabezalEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioCabezalEmpresa> DeleteSubsidioCabezalEmpresa(int subsidiocabezalempresaid)
        {
            var itemToDelete = Context.SubsidioCabezalEmpresas
                              .Where(i => i.SubsidioCabezalempresaId == subsidiocabezalempresaid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioCabezalEmpresaDeleted(itemToDelete);


            Context.SubsidioCabezalEmpresas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioCabezalEmpresaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioEnfermedadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioenfermedads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioenfermedads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioEnfermedadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioenfermedads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioenfermedads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioEnfermedadsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad>> GetSubsidioEnfermedads(Query query = null)
        {
            var items = Context.SubsidioEnfermedads.AsQueryable();

            items = items.Include(i => i.SubsidioCabezal);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioEnfermedadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioEnfermedadGet(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnGetSubsidioEnfermedadBySubsidioEnfermedadId(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> GetSubsidioEnfermedadBySubsidioEnfermedadId(int subsidioenfermedadid)
        {
            var items = Context.SubsidioEnfermedads
                              .AsNoTracking()
                              .Where(i => i.SubsidioEnfermedadId == subsidioenfermedadid);

            items = items.Include(i => i.SubsidioCabezal);
 
            OnGetSubsidioEnfermedadBySubsidioEnfermedadId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioEnfermedadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioEnfermedadCreated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnAfterSubsidioEnfermedadCreated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> CreateSubsidioEnfermedad(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad subsidioenfermedad)
        {
            OnSubsidioEnfermedadCreated(subsidioenfermedad);

            var existingItem = Context.SubsidioEnfermedads
                              .Where(i => i.SubsidioEnfermedadId == subsidioenfermedad.SubsidioEnfermedadId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioEnfermedads.Add(subsidioenfermedad);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidioenfermedad).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioEnfermedadCreated(subsidioenfermedad);

            return subsidioenfermedad;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> CancelSubsidioEnfermedadChanges(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioEnfermedadUpdated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnAfterSubsidioEnfermedadUpdated(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> UpdateSubsidioEnfermedad(int subsidioenfermedadid, SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad subsidioenfermedad)
        {
            OnSubsidioEnfermedadUpdated(subsidioenfermedad);

            var itemToUpdate = Context.SubsidioEnfermedads
                              .Where(i => i.SubsidioEnfermedadId == subsidioenfermedad.SubsidioEnfermedadId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidioenfermedad);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioEnfermedadUpdated(subsidioenfermedad);

            return subsidioenfermedad;
        }

        partial void OnSubsidioEnfermedadDeleted(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);
        partial void OnAfterSubsidioEnfermedadDeleted(SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioEnfermedad> DeleteSubsidioEnfermedad(int subsidioenfermedadid)
        {
            var itemToDelete = Context.SubsidioEnfermedads
                              .Where(i => i.SubsidioEnfermedadId == subsidioenfermedadid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioEnfermedadDeleted(itemToDelete);


            Context.SubsidioEnfermedads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioEnfermedadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioImponiblesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioimponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioimponibles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioImponiblesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioimponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioimponibles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioImponiblesRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioImponible> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioImponible>> GetSubsidioImponibles(Query query = null)
        {
            var items = Context.SubsidioImponibles.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioImponiblesRead(ref items);

            return await Task.FromResult(items);
        }

        public async Task ExportSubsidioItemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioItemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioItemsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItem> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItem>> GetSubsidioItems(Query query = null)
        {
            var items = Context.SubsidioItems.AsQueryable();

            items = items.Include(i => i.SubsidioItemCod);
            items = items.Include(i => i.SubsidioCabezal);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioItemsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioItemGet(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnGetSubsidioItemBySubsidioItemId(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItem> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> GetSubsidioItemBySubsidioItemId(int subsidioitemid)
        {
            var items = Context.SubsidioItems
                              .AsNoTracking()
                              .Where(i => i.SubsidioItemId == subsidioitemid);

            items = items.Include(i => i.SubsidioItemCod);
            items = items.Include(i => i.SubsidioCabezal);
 
            OnGetSubsidioItemBySubsidioItemId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioItemGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioItemCreated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnAfterSubsidioItemCreated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> CreateSubsidioItem(SgpaNew.Server.Models.Sgpa.SubsidioItem subsidioitem)
        {
            OnSubsidioItemCreated(subsidioitem);

            var existingItem = Context.SubsidioItems
                              .Where(i => i.SubsidioItemId == subsidioitem.SubsidioItemId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioItems.Add(subsidioitem);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidioitem).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioItemCreated(subsidioitem);

            return subsidioitem;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> CancelSubsidioItemChanges(SgpaNew.Server.Models.Sgpa.SubsidioItem item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioItemUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnAfterSubsidioItemUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItem item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> UpdateSubsidioItem(int subsidioitemid, SgpaNew.Server.Models.Sgpa.SubsidioItem subsidioitem)
        {
            OnSubsidioItemUpdated(subsidioitem);

            var itemToUpdate = Context.SubsidioItems
                              .Where(i => i.SubsidioItemId == subsidioitem.SubsidioItemId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidioitem);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioItemUpdated(subsidioitem);

            return subsidioitem;
        }

        partial void OnSubsidioItemDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItem item);
        partial void OnAfterSubsidioItemDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItem item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItem> DeleteSubsidioItem(int subsidioitemid)
        {
            var itemToDelete = Context.SubsidioItems
                              .Where(i => i.SubsidioItemId == subsidioitemid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioItemDeleted(itemToDelete);


            Context.SubsidioItems.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioItemDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioItemCodsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcods/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcods/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioItemCodsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcods/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcods/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioItemCodsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod>> GetSubsidioItemCods(Query query = null)
        {
            var items = Context.SubsidioItemCods.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioItemCodsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioItemCodGet(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnGetSubsidioItemCodByCodSubsidioItemCod(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> GetSubsidioItemCodByCodSubsidioItemCod(short codsubsidioitemcod)
        {
            var items = Context.SubsidioItemCods
                              .AsNoTracking()
                              .Where(i => i.CodSubsidioItemCod == codsubsidioitemcod);

 
            OnGetSubsidioItemCodByCodSubsidioItemCod(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioItemCodGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioItemCodCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnAfterSubsidioItemCodCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> CreateSubsidioItemCod(SgpaNew.Server.Models.Sgpa.SubsidioItemCod subsidioitemcod)
        {
            OnSubsidioItemCodCreated(subsidioitemcod);

            var existingItem = Context.SubsidioItemCods
                              .Where(i => i.CodSubsidioItemCod == subsidioitemcod.CodSubsidioItemCod)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioItemCods.Add(subsidioitemcod);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidioitemcod).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioItemCodCreated(subsidioitemcod);

            return subsidioitemcod;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> CancelSubsidioItemCodChanges(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioItemCodUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnAfterSubsidioItemCodUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> UpdateSubsidioItemCod(short codsubsidioitemcod, SgpaNew.Server.Models.Sgpa.SubsidioItemCod subsidioitemcod)
        {
            OnSubsidioItemCodUpdated(subsidioitemcod);

            var itemToUpdate = Context.SubsidioItemCods
                              .Where(i => i.CodSubsidioItemCod == subsidioitemcod.CodSubsidioItemCod)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidioitemcod);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioItemCodUpdated(subsidioitemcod);

            return subsidioitemcod;
        }

        partial void OnSubsidioItemCodDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);
        partial void OnAfterSubsidioItemCodDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemCod item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemCod> DeleteSubsidioItemCod(short codsubsidioitemcod)
        {
            var itemToDelete = Context.SubsidioItemCods
                              .Where(i => i.CodSubsidioItemCod == codsubsidioitemcod)
                              .Include(i => i.SubsidioItems)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioItemCodDeleted(itemToDelete);


            Context.SubsidioItemCods.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioItemCodDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioitemcodAfiliadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcodafiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcodafiliados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioitemcodAfiliadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemcodafiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemcodafiliados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioitemcodAfiliadosRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado>> GetSubsidioitemcodAfiliados(Query query = null)
        {
            var items = Context.SubsidioitemcodAfiliados.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioitemcodAfiliadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioitemcodAfiliadoGet(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnGetSubsidioitemcodAfiliadoBySubItmCodAfiId(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> GetSubsidioitemcodAfiliadoBySubItmCodAfiId(int subitmcodafiid)
        {
            var items = Context.SubsidioitemcodAfiliados
                              .AsNoTracking()
                              .Where(i => i.SubItmCodAfiId == subitmcodafiid);

 
            OnGetSubsidioitemcodAfiliadoBySubItmCodAfiId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioitemcodAfiliadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioitemcodAfiliadoCreated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnAfterSubsidioitemcodAfiliadoCreated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> CreateSubsidioitemcodAfiliado(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado subsidioitemcodafiliado)
        {
            OnSubsidioitemcodAfiliadoCreated(subsidioitemcodafiliado);

            var existingItem = Context.SubsidioitemcodAfiliados
                              .Where(i => i.SubItmCodAfiId == subsidioitemcodafiliado.SubItmCodAfiId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioitemcodAfiliados.Add(subsidioitemcodafiliado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidioitemcodafiliado).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioitemcodAfiliadoCreated(subsidioitemcodafiliado);

            return subsidioitemcodafiliado;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> CancelSubsidioitemcodAfiliadoChanges(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioitemcodAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnAfterSubsidioitemcodAfiliadoUpdated(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> UpdateSubsidioitemcodAfiliado(int subitmcodafiid, SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado subsidioitemcodafiliado)
        {
            OnSubsidioitemcodAfiliadoUpdated(subsidioitemcodafiliado);

            var itemToUpdate = Context.SubsidioitemcodAfiliados
                              .Where(i => i.SubItmCodAfiId == subsidioitemcodafiliado.SubItmCodAfiId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidioitemcodafiliado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioitemcodAfiliadoUpdated(subsidioitemcodafiliado);

            return subsidioitemcodafiliado;
        }

        partial void OnSubsidioitemcodAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);
        partial void OnAfterSubsidioitemcodAfiliadoDeleted(SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioitemcodAfiliado> DeleteSubsidioitemcodAfiliado(int subitmcodafiid)
        {
            var itemToDelete = Context.SubsidioitemcodAfiliados
                              .Where(i => i.SubItmCodAfiId == subitmcodafiid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioitemcodAfiliadoDeleted(itemToDelete);


            Context.SubsidioitemcodAfiliados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioitemcodAfiliadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSubsidioItemEmpresasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemempresas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSubsidioItemEmpresasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/subsidioitemempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/subsidioitemempresas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSubsidioItemEmpresasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa>> GetSubsidioItemEmpresas(Query query = null)
        {
            var items = Context.SubsidioItemEmpresas.AsQueryable();

            items = items.Include(i => i.Empresa);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSubsidioItemEmpresasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSubsidioItemEmpresaGet(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnGetSubsidioItemEmpresaBySubsidioItemEmpresaId(ref IQueryable<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> items);


        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> GetSubsidioItemEmpresaBySubsidioItemEmpresaId(int subsidioitemempresaid)
        {
            var items = Context.SubsidioItemEmpresas
                              .AsNoTracking()
                              .Where(i => i.SubsidioItemEmpresaId == subsidioitemempresaid);

            items = items.Include(i => i.Empresa);
 
            OnGetSubsidioItemEmpresaBySubsidioItemEmpresaId(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSubsidioItemEmpresaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSubsidioItemEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnAfterSubsidioItemEmpresaCreated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> CreateSubsidioItemEmpresa(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa subsidioitemempresa)
        {
            OnSubsidioItemEmpresaCreated(subsidioitemempresa);

            var existingItem = Context.SubsidioItemEmpresas
                              .Where(i => i.SubsidioItemEmpresaId == subsidioitemempresa.SubsidioItemEmpresaId)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SubsidioItemEmpresas.Add(subsidioitemempresa);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(subsidioitemempresa).State = EntityState.Detached;
                throw;
            }

            OnAfterSubsidioItemEmpresaCreated(subsidioitemempresa);

            return subsidioitemempresa;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> CancelSubsidioItemEmpresaChanges(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSubsidioItemEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnAfterSubsidioItemEmpresaUpdated(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> UpdateSubsidioItemEmpresa(int subsidioitemempresaid, SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa subsidioitemempresa)
        {
            OnSubsidioItemEmpresaUpdated(subsidioitemempresa);

            var itemToUpdate = Context.SubsidioItemEmpresas
                              .Where(i => i.SubsidioItemEmpresaId == subsidioitemempresa.SubsidioItemEmpresaId)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(subsidioitemempresa);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSubsidioItemEmpresaUpdated(subsidioitemempresa);

            return subsidioitemempresa;
        }

        partial void OnSubsidioItemEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);
        partial void OnAfterSubsidioItemEmpresaDeleted(SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa item);

        public async Task<SgpaNew.Server.Models.Sgpa.SubsidioItemEmpresa> DeleteSubsidioItemEmpresa(int subsidioitemempresaid)
        {
            var itemToDelete = Context.SubsidioItemEmpresas
                              .Where(i => i.SubsidioItemEmpresaId == subsidioitemempresaid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSubsidioItemEmpresaDeleted(itemToDelete);


            Context.SubsidioItemEmpresas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSubsidioItemEmpresaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTrabajasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/trabajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/trabajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTrabajasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/trabajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/trabajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTrabajasRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.Trabaja> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.Trabaja>> GetTrabajas(Query query = null)
        {
            var items = Context.Trabajas.AsQueryable();

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.BajaMotivo);
            items = items.Include(i => i.Empresa);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTrabajasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTrabajaGet(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnGetTrabajaByIdTrabaja(ref IQueryable<SgpaNew.Server.Models.Sgpa.Trabaja> items);


        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> GetTrabajaByIdTrabaja(int idtrabaja)
        {
            var items = Context.Trabajas
                              .AsNoTracking()
                              .Where(i => i.IdTrabaja == idtrabaja);

            items = items.Include(i => i.Afiliado);
            items = items.Include(i => i.BajaMotivo);
            items = items.Include(i => i.Empresa);
 
            OnGetTrabajaByIdTrabaja(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTrabajaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTrabajaCreated(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnAfterTrabajaCreated(SgpaNew.Server.Models.Sgpa.Trabaja item);

        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> CreateTrabaja(SgpaNew.Server.Models.Sgpa.Trabaja trabaja)
        {
            OnTrabajaCreated(trabaja);

            var existingItem = Context.Trabajas
                              .Where(i => i.IdTrabaja == trabaja.IdTrabaja)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Trabajas.Add(trabaja);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(trabaja).State = EntityState.Detached;
                throw;
            }

            OnAfterTrabajaCreated(trabaja);

            return trabaja;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> CancelTrabajaChanges(SgpaNew.Server.Models.Sgpa.Trabaja item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTrabajaUpdated(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnAfterTrabajaUpdated(SgpaNew.Server.Models.Sgpa.Trabaja item);

        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> UpdateTrabaja(int idtrabaja, SgpaNew.Server.Models.Sgpa.Trabaja trabaja)
        {
            OnTrabajaUpdated(trabaja);

            var itemToUpdate = Context.Trabajas
                              .Where(i => i.IdTrabaja == trabaja.IdTrabaja)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(trabaja);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTrabajaUpdated(trabaja);

            return trabaja;
        }

        partial void OnTrabajaDeleted(SgpaNew.Server.Models.Sgpa.Trabaja item);
        partial void OnAfterTrabajaDeleted(SgpaNew.Server.Models.Sgpa.Trabaja item);

        public async Task<SgpaNew.Server.Models.Sgpa.Trabaja> DeleteTrabaja(int idtrabaja)
        {
            var itemToDelete = Context.Trabajas
                              .Where(i => i.IdTrabaja == idtrabaja)
                              .Include(i => i.Imponibles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTrabajaDeleted(itemToDelete);


            Context.Trabajas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTrabajaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXUsrParamsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/xusrparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/xusrparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXUsrParamsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/sgpa/xusrparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/sgpa/xusrparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXUsrParamsRead(ref IQueryable<SgpaNew.Server.Models.Sgpa.XUsrParam> items);

        public async Task<IQueryable<SgpaNew.Server.Models.Sgpa.XUsrParam>> GetXUsrParams(Query query = null)
        {
            var items = Context.XUsrParams.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnXUsrParamsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXUsrParamGet(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnGetXUsrParamByIdUsuario(ref IQueryable<SgpaNew.Server.Models.Sgpa.XUsrParam> items);


        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> GetXUsrParamByIdUsuario(int idusuario)
        {
            var items = Context.XUsrParams
                              .AsNoTracking()
                              .Where(i => i.IdUsuario == idusuario);

 
            OnGetXUsrParamByIdUsuario(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXUsrParamGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXUsrParamCreated(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnAfterXUsrParamCreated(SgpaNew.Server.Models.Sgpa.XUsrParam item);

        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> CreateXUsrParam(SgpaNew.Server.Models.Sgpa.XUsrParam xusrparam)
        {
            OnXUsrParamCreated(xusrparam);

            var existingItem = Context.XUsrParams
                              .Where(i => i.IdUsuario == xusrparam.IdUsuario)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XUsrParams.Add(xusrparam);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xusrparam).State = EntityState.Detached;
                throw;
            }

            OnAfterXUsrParamCreated(xusrparam);

            return xusrparam;
        }

        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> CancelXUsrParamChanges(SgpaNew.Server.Models.Sgpa.XUsrParam item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXUsrParamUpdated(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnAfterXUsrParamUpdated(SgpaNew.Server.Models.Sgpa.XUsrParam item);

        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> UpdateXUsrParam(int idusuario, SgpaNew.Server.Models.Sgpa.XUsrParam xusrparam)
        {
            OnXUsrParamUpdated(xusrparam);

            var itemToUpdate = Context.XUsrParams
                              .Where(i => i.IdUsuario == xusrparam.IdUsuario)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xusrparam);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXUsrParamUpdated(xusrparam);

            return xusrparam;
        }

        partial void OnXUsrParamDeleted(SgpaNew.Server.Models.Sgpa.XUsrParam item);
        partial void OnAfterXUsrParamDeleted(SgpaNew.Server.Models.Sgpa.XUsrParam item);

        public async Task<SgpaNew.Server.Models.Sgpa.XUsrParam> DeleteXUsrParam(int idusuario)
        {
            var itemToDelete = Context.XUsrParams
                              .Where(i => i.IdUsuario == idusuario)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXUsrParamDeleted(itemToDelete);


            Context.XUsrParams.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXUsrParamDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}