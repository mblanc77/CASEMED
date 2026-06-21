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

using SGPA.Server.Data;

namespace SGPA.Server
{
    public partial class CMUService
    {
        CMUContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly CMUContext context;
        private readonly NavigationManager navigationManager;

        public CMUService(CMUContext context, NavigationManager navigationManager)
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


        public async Task ExportActaConsejosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/actaconsejos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/actaconsejos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportActaConsejosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/actaconsejos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/actaconsejos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnActaConsejosRead(ref IQueryable<SGPA.Server.Models.CMU.ActaConsejo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ActaConsejo>> GetActaConsejos(Query query = null)
        {
            var items = Context.ActaConsejos.AsQueryable();

            items = items.Include(i => i.FileDatum);

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

            OnActaConsejosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnActaConsejoGet(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnGetActaConsejoById(ref IQueryable<SGPA.Server.Models.CMU.ActaConsejo> items);


        public async Task<SGPA.Server.Models.CMU.ActaConsejo> GetActaConsejoById(int id)
        {
            var items = Context.ActaConsejos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.FileDatum);
 
            OnGetActaConsejoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnActaConsejoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnActaConsejoCreated(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnAfterActaConsejoCreated(SGPA.Server.Models.CMU.ActaConsejo item);

        public async Task<SGPA.Server.Models.CMU.ActaConsejo> CreateActaConsejo(SGPA.Server.Models.CMU.ActaConsejo actaconsejo)
        {
            OnActaConsejoCreated(actaconsejo);

            var existingItem = Context.ActaConsejos
                              .Where(i => i.Id == actaconsejo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ActaConsejos.Add(actaconsejo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(actaconsejo).State = EntityState.Detached;
                throw;
            }

            OnAfterActaConsejoCreated(actaconsejo);

            return actaconsejo;
        }

        public async Task<SGPA.Server.Models.CMU.ActaConsejo> CancelActaConsejoChanges(SGPA.Server.Models.CMU.ActaConsejo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnActaConsejoUpdated(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnAfterActaConsejoUpdated(SGPA.Server.Models.CMU.ActaConsejo item);

        public async Task<SGPA.Server.Models.CMU.ActaConsejo> UpdateActaConsejo(int id, SGPA.Server.Models.CMU.ActaConsejo actaconsejo)
        {
            OnActaConsejoUpdated(actaconsejo);

            var itemToUpdate = Context.ActaConsejos
                              .Where(i => i.Id == actaconsejo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(actaconsejo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterActaConsejoUpdated(actaconsejo);

            return actaconsejo;
        }

        partial void OnActaConsejoDeleted(SGPA.Server.Models.CMU.ActaConsejo item);
        partial void OnAfterActaConsejoDeleted(SGPA.Server.Models.CMU.ActaConsejo item);

        public async Task<SGPA.Server.Models.CMU.ActaConsejo> DeleteActaConsejo(int id)
        {
            var itemToDelete = Context.ActaConsejos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnActaConsejoDeleted(itemToDelete);


            Context.ActaConsejos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterActaConsejoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAgenteCobranzasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAgenteCobranzasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAgenteCobranzasRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranza> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AgenteCobranza>> GetAgenteCobranzas(Query query = null)
        {
            var items = Context.AgenteCobranzas.AsQueryable();

            items = items.Include(i => i.AgenteCobranzaTipo);
            items = items.Include(i => i.CuentaBancarium);
            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.AgenteGrupo);
            items = items.Include(i => i.OrigenMovimiento);
            items = items.Include(i => i.Region1);

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

            OnAgenteCobranzasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAgenteCobranzaGet(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnGetAgenteCobranzaById(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranza> items);


        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> GetAgenteCobranzaById(int id)
        {
            var items = Context.AgenteCobranzas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranzaTipo);
            items = items.Include(i => i.CuentaBancarium);
            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.AgenteGrupo);
            items = items.Include(i => i.OrigenMovimiento);
            items = items.Include(i => i.Region1);
 
            OnGetAgenteCobranzaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAgenteCobranzaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAgenteCobranzaCreated(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnAfterAgenteCobranzaCreated(SGPA.Server.Models.CMU.AgenteCobranza item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> CreateAgenteCobranza(SGPA.Server.Models.CMU.AgenteCobranza agentecobranza)
        {
            OnAgenteCobranzaCreated(agentecobranza);

            var existingItem = Context.AgenteCobranzas
                              .Where(i => i.Id == agentecobranza.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AgenteCobranzas.Add(agentecobranza);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(agentecobranza).State = EntityState.Detached;
                throw;
            }

            OnAfterAgenteCobranzaCreated(agentecobranza);

            return agentecobranza;
        }

        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> CancelAgenteCobranzaChanges(SGPA.Server.Models.CMU.AgenteCobranza item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAgenteCobranzaUpdated(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnAfterAgenteCobranzaUpdated(SGPA.Server.Models.CMU.AgenteCobranza item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> UpdateAgenteCobranza(int id, SGPA.Server.Models.CMU.AgenteCobranza agentecobranza)
        {
            OnAgenteCobranzaUpdated(agentecobranza);

            var itemToUpdate = Context.AgenteCobranzas
                              .Where(i => i.Id == agentecobranza.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(agentecobranza);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAgenteCobranzaUpdated(agentecobranza);

            return agentecobranza;
        }

        partial void OnAgenteCobranzaDeleted(SGPA.Server.Models.CMU.AgenteCobranza item);
        partial void OnAfterAgenteCobranzaDeleted(SGPA.Server.Models.CMU.AgenteCobranza item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranza> DeleteAgenteCobranza(int id)
        {
            var itemToDelete = Context.AgenteCobranzas
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzaDebitos)
                              .Include(i => i.Cobros)
                              .Include(i => i.Colegiados)
                              .Include(i => i.Colegiados1)
                              .Include(i => i.Parametros)
                              .Include(i => i.UsuarioInstitucions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAgenteCobranzaDeleted(itemToDelete);


            Context.AgenteCobranzas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAgenteCobranzaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAgenteCobranzaDebitosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzadebitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzadebitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAgenteCobranzaDebitosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzadebitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzadebitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAgenteCobranzaDebitosRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaDebito>> GetAgenteCobranzaDebitos(Query query = null)
        {
            var items = Context.AgenteCobranzaDebitos.AsQueryable();

            items = items.Include(i => i.AgenteCobranza);

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

            OnAgenteCobranzaDebitosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAgenteCobranzaDebitoGet(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnGetAgenteCobranzaDebitoById(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaDebito> items);


        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> GetAgenteCobranzaDebitoById(int id)
        {
            var items = Context.AgenteCobranzaDebitos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranza);
 
            OnGetAgenteCobranzaDebitoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAgenteCobranzaDebitoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAgenteCobranzaDebitoCreated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnAfterAgenteCobranzaDebitoCreated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> CreateAgenteCobranzaDebito(SGPA.Server.Models.CMU.AgenteCobranzaDebito agentecobranzadebito)
        {
            OnAgenteCobranzaDebitoCreated(agentecobranzadebito);

            var existingItem = Context.AgenteCobranzaDebitos
                              .Where(i => i.Id == agentecobranzadebito.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AgenteCobranzaDebitos.Add(agentecobranzadebito);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(agentecobranzadebito).State = EntityState.Detached;
                throw;
            }

            OnAfterAgenteCobranzaDebitoCreated(agentecobranzadebito);

            return agentecobranzadebito;
        }

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> CancelAgenteCobranzaDebitoChanges(SGPA.Server.Models.CMU.AgenteCobranzaDebito item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAgenteCobranzaDebitoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnAfterAgenteCobranzaDebitoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> UpdateAgenteCobranzaDebito(int id, SGPA.Server.Models.CMU.AgenteCobranzaDebito agentecobranzadebito)
        {
            OnAgenteCobranzaDebitoUpdated(agentecobranzadebito);

            var itemToUpdate = Context.AgenteCobranzaDebitos
                              .Where(i => i.Id == agentecobranzadebito.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(agentecobranzadebito);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAgenteCobranzaDebitoUpdated(agentecobranzadebito);

            return agentecobranzadebito;
        }

        partial void OnAgenteCobranzaDebitoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);
        partial void OnAfterAgenteCobranzaDebitoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaDebito item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaDebito> DeleteAgenteCobranzaDebito(int id)
        {
            var itemToDelete = Context.AgenteCobranzaDebitos
                              .Where(i => i.Id == id)
                              .Include(i => i.ColegiadoDebitoBancarioAsociados)
                              .Include(i => i.ColegiadoTarjetaDebitoAsociada)
                              .Include(i => i.Debitos)
                              .Include(i => i.Parametros)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAgenteCobranzaDebitoDeleted(itemToDelete);


            Context.AgenteCobranzaDebitos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAgenteCobranzaDebitoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAgenteCobranzaTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAgenteCobranzaTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentecobranzatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentecobranzatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAgenteCobranzaTiposRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaTipo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaTipo>> GetAgenteCobranzaTipos(Query query = null)
        {
            var items = Context.AgenteCobranzaTipos.AsQueryable();


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

            OnAgenteCobranzaTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAgenteCobranzaTipoGet(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnGetAgenteCobranzaTipoById(ref IQueryable<SGPA.Server.Models.CMU.AgenteCobranzaTipo> items);


        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> GetAgenteCobranzaTipoById(int id)
        {
            var items = Context.AgenteCobranzaTipos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAgenteCobranzaTipoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAgenteCobranzaTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAgenteCobranzaTipoCreated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnAfterAgenteCobranzaTipoCreated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> CreateAgenteCobranzaTipo(SGPA.Server.Models.CMU.AgenteCobranzaTipo agentecobranzatipo)
        {
            OnAgenteCobranzaTipoCreated(agentecobranzatipo);

            var existingItem = Context.AgenteCobranzaTipos
                              .Where(i => i.Id == agentecobranzatipo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AgenteCobranzaTipos.Add(agentecobranzatipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(agentecobranzatipo).State = EntityState.Detached;
                throw;
            }

            OnAfterAgenteCobranzaTipoCreated(agentecobranzatipo);

            return agentecobranzatipo;
        }

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> CancelAgenteCobranzaTipoChanges(SGPA.Server.Models.CMU.AgenteCobranzaTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAgenteCobranzaTipoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnAfterAgenteCobranzaTipoUpdated(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> UpdateAgenteCobranzaTipo(int id, SGPA.Server.Models.CMU.AgenteCobranzaTipo agentecobranzatipo)
        {
            OnAgenteCobranzaTipoUpdated(agentecobranzatipo);

            var itemToUpdate = Context.AgenteCobranzaTipos
                              .Where(i => i.Id == agentecobranzatipo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(agentecobranzatipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAgenteCobranzaTipoUpdated(agentecobranzatipo);

            return agentecobranzatipo;
        }

        partial void OnAgenteCobranzaTipoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);
        partial void OnAfterAgenteCobranzaTipoDeleted(SGPA.Server.Models.CMU.AgenteCobranzaTipo item);

        public async Task<SGPA.Server.Models.CMU.AgenteCobranzaTipo> DeleteAgenteCobranzaTipo(int id)
        {
            var itemToDelete = Context.AgenteCobranzaTipos
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAgenteCobranzaTipoDeleted(itemToDelete);


            Context.AgenteCobranzaTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAgenteCobranzaTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAgenteGruposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentegrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentegrupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAgenteGruposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/agentegrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/agentegrupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAgenteGruposRead(ref IQueryable<SGPA.Server.Models.CMU.AgenteGrupo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AgenteGrupo>> GetAgenteGrupos(Query query = null)
        {
            var items = Context.AgenteGrupos.AsQueryable();


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

            OnAgenteGruposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAgenteGrupoGet(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnGetAgenteGrupoById(ref IQueryable<SGPA.Server.Models.CMU.AgenteGrupo> items);


        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> GetAgenteGrupoById(int id)
        {
            var items = Context.AgenteGrupos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAgenteGrupoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAgenteGrupoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAgenteGrupoCreated(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnAfterAgenteGrupoCreated(SGPA.Server.Models.CMU.AgenteGrupo item);

        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> CreateAgenteGrupo(SGPA.Server.Models.CMU.AgenteGrupo agentegrupo)
        {
            OnAgenteGrupoCreated(agentegrupo);

            var existingItem = Context.AgenteGrupos
                              .Where(i => i.Id == agentegrupo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AgenteGrupos.Add(agentegrupo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(agentegrupo).State = EntityState.Detached;
                throw;
            }

            OnAfterAgenteGrupoCreated(agentegrupo);

            return agentegrupo;
        }

        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> CancelAgenteGrupoChanges(SGPA.Server.Models.CMU.AgenteGrupo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAgenteGrupoUpdated(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnAfterAgenteGrupoUpdated(SGPA.Server.Models.CMU.AgenteGrupo item);

        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> UpdateAgenteGrupo(int id, SGPA.Server.Models.CMU.AgenteGrupo agentegrupo)
        {
            OnAgenteGrupoUpdated(agentegrupo);

            var itemToUpdate = Context.AgenteGrupos
                              .Where(i => i.Id == agentegrupo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(agentegrupo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAgenteGrupoUpdated(agentegrupo);

            return agentegrupo;
        }

        partial void OnAgenteGrupoDeleted(SGPA.Server.Models.CMU.AgenteGrupo item);
        partial void OnAfterAgenteGrupoDeleted(SGPA.Server.Models.CMU.AgenteGrupo item);

        public async Task<SGPA.Server.Models.CMU.AgenteGrupo> DeleteAgenteGrupo(int id)
        {
            var itemToDelete = Context.AgenteGrupos
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAgenteGrupoDeleted(itemToDelete);


            Context.AgenteGrupos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAgenteGrupoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAjusteDetallesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajustedetalles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajustedetalles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAjusteDetallesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajustedetalles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajustedetalles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAjusteDetallesRead(ref IQueryable<SGPA.Server.Models.CMU.AjusteDetalle> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AjusteDetalle>> GetAjusteDetalles(Query query = null)
        {
            var items = Context.AjusteDetalles.AsQueryable();

            items = items.Include(i => i.AjusteRetroactivo);

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

            OnAjusteDetallesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAjusteDetalleGet(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnGetAjusteDetalleById(ref IQueryable<SGPA.Server.Models.CMU.AjusteDetalle> items);


        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> GetAjusteDetalleById(int id)
        {
            var items = Context.AjusteDetalles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AjusteRetroactivo);
 
            OnGetAjusteDetalleById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAjusteDetalleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAjusteDetalleCreated(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnAfterAjusteDetalleCreated(SGPA.Server.Models.CMU.AjusteDetalle item);

        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> CreateAjusteDetalle(SGPA.Server.Models.CMU.AjusteDetalle ajustedetalle)
        {
            OnAjusteDetalleCreated(ajustedetalle);

            var existingItem = Context.AjusteDetalles
                              .Where(i => i.Id == ajustedetalle.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AjusteDetalles.Add(ajustedetalle);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ajustedetalle).State = EntityState.Detached;
                throw;
            }

            OnAfterAjusteDetalleCreated(ajustedetalle);

            return ajustedetalle;
        }

        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> CancelAjusteDetalleChanges(SGPA.Server.Models.CMU.AjusteDetalle item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAjusteDetalleUpdated(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnAfterAjusteDetalleUpdated(SGPA.Server.Models.CMU.AjusteDetalle item);

        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> UpdateAjusteDetalle(int id, SGPA.Server.Models.CMU.AjusteDetalle ajustedetalle)
        {
            OnAjusteDetalleUpdated(ajustedetalle);

            var itemToUpdate = Context.AjusteDetalles
                              .Where(i => i.Id == ajustedetalle.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ajustedetalle);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAjusteDetalleUpdated(ajustedetalle);

            return ajustedetalle;
        }

        partial void OnAjusteDetalleDeleted(SGPA.Server.Models.CMU.AjusteDetalle item);
        partial void OnAfterAjusteDetalleDeleted(SGPA.Server.Models.CMU.AjusteDetalle item);

        public async Task<SGPA.Server.Models.CMU.AjusteDetalle> DeleteAjusteDetalle(int id)
        {
            var itemToDelete = Context.AjusteDetalles
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAjusteDetalleDeleted(itemToDelete);


            Context.AjusteDetalles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAjusteDetalleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAjusteRetroactivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajusteretroactivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajusteretroactivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAjusteRetroactivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/ajusteretroactivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/ajusteretroactivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAjusteRetroactivosRead(ref IQueryable<SGPA.Server.Models.CMU.AjusteRetroactivo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AjusteRetroactivo>> GetAjusteRetroactivos(Query query = null)
        {
            var items = Context.AjusteRetroactivos.AsQueryable();

            items = items.Include(i => i.AjusteRetroactivo1);
            items = items.Include(i => i.ColegiadoCambioCategorium);
            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.ColegiadoDeclaracionJuradum);

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

            OnAjusteRetroactivosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAjusteRetroactivoGet(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnGetAjusteRetroactivoById(ref IQueryable<SGPA.Server.Models.CMU.AjusteRetroactivo> items);


        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> GetAjusteRetroactivoById(int id)
        {
            var items = Context.AjusteRetroactivos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AjusteRetroactivo1);
            items = items.Include(i => i.ColegiadoCambioCategorium);
            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.ColegiadoDeclaracionJuradum);
 
            OnGetAjusteRetroactivoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAjusteRetroactivoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAjusteRetroactivoCreated(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnAfterAjusteRetroactivoCreated(SGPA.Server.Models.CMU.AjusteRetroactivo item);

        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> CreateAjusteRetroactivo(SGPA.Server.Models.CMU.AjusteRetroactivo ajusteretroactivo)
        {
            OnAjusteRetroactivoCreated(ajusteretroactivo);

            var existingItem = Context.AjusteRetroactivos
                              .Where(i => i.Id == ajusteretroactivo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AjusteRetroactivos.Add(ajusteretroactivo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ajusteretroactivo).State = EntityState.Detached;
                throw;
            }

            OnAfterAjusteRetroactivoCreated(ajusteretroactivo);

            return ajusteretroactivo;
        }

        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> CancelAjusteRetroactivoChanges(SGPA.Server.Models.CMU.AjusteRetroactivo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAjusteRetroactivoUpdated(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnAfterAjusteRetroactivoUpdated(SGPA.Server.Models.CMU.AjusteRetroactivo item);

        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> UpdateAjusteRetroactivo(int id, SGPA.Server.Models.CMU.AjusteRetroactivo ajusteretroactivo)
        {
            OnAjusteRetroactivoUpdated(ajusteretroactivo);

            var itemToUpdate = Context.AjusteRetroactivos
                              .Where(i => i.Id == ajusteretroactivo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ajusteretroactivo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAjusteRetroactivoUpdated(ajusteretroactivo);

            return ajusteretroactivo;
        }

        partial void OnAjusteRetroactivoDeleted(SGPA.Server.Models.CMU.AjusteRetroactivo item);
        partial void OnAfterAjusteRetroactivoDeleted(SGPA.Server.Models.CMU.AjusteRetroactivo item);

        public async Task<SGPA.Server.Models.CMU.AjusteRetroactivo> DeleteAjusteRetroactivo(int id)
        {
            var itemToDelete = Context.AjusteRetroactivos
                              .Where(i => i.Id == id)
                              .Include(i => i.AjusteDetalles)
                              .Include(i => i.AjusteRetroactivos1)
                              .Include(i => i.MovimientoCuenta)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAjusteRetroactivoDeleted(itemToDelete);


            Context.AjusteRetroactivos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAjusteRetroactivoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAnalysesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/analyses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/analyses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAnalysesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/analyses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/analyses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAnalysesRead(ref IQueryable<SGPA.Server.Models.CMU.Analysis> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Analysis>> GetAnalyses(Query query = null)
        {
            var items = Context.Analyses.AsQueryable();


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

            OnAnalysesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAnalysisGet(SGPA.Server.Models.CMU.Analysis item);
        partial void OnGetAnalysisByOid(ref IQueryable<SGPA.Server.Models.CMU.Analysis> items);


        public async Task<SGPA.Server.Models.CMU.Analysis> GetAnalysisByOid(Guid oid)
        {
            var items = Context.Analyses
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetAnalysisByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAnalysisGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAnalysisCreated(SGPA.Server.Models.CMU.Analysis item);
        partial void OnAfterAnalysisCreated(SGPA.Server.Models.CMU.Analysis item);

        public async Task<SGPA.Server.Models.CMU.Analysis> CreateAnalysis(SGPA.Server.Models.CMU.Analysis analysis)
        {
            OnAnalysisCreated(analysis);

            var existingItem = Context.Analyses
                              .Where(i => i.Oid == analysis.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Analyses.Add(analysis);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(analysis).State = EntityState.Detached;
                throw;
            }

            OnAfterAnalysisCreated(analysis);

            return analysis;
        }

        public async Task<SGPA.Server.Models.CMU.Analysis> CancelAnalysisChanges(SGPA.Server.Models.CMU.Analysis item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAnalysisUpdated(SGPA.Server.Models.CMU.Analysis item);
        partial void OnAfterAnalysisUpdated(SGPA.Server.Models.CMU.Analysis item);

        public async Task<SGPA.Server.Models.CMU.Analysis> UpdateAnalysis(Guid oid, SGPA.Server.Models.CMU.Analysis analysis)
        {
            OnAnalysisUpdated(analysis);

            var itemToUpdate = Context.Analyses
                              .Where(i => i.Oid == analysis.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(analysis);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAnalysisUpdated(analysis);

            return analysis;
        }

        partial void OnAnalysisDeleted(SGPA.Server.Models.CMU.Analysis item);
        partial void OnAfterAnalysisDeleted(SGPA.Server.Models.CMU.Analysis item);

        public async Task<SGPA.Server.Models.CMU.Analysis> DeleteAnalysis(Guid oid)
        {
            var itemToDelete = Context.Analyses
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAnalysisDeleted(itemToDelete);


            Context.Analyses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAnalysisDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAreaContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/areacontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/areacontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAreaContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/areacontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/areacontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAreaContactosRead(ref IQueryable<SGPA.Server.Models.CMU.AreaContacto> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AreaContacto>> GetAreaContactos(Query query = null)
        {
            var items = Context.AreaContactos.AsQueryable();


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

            OnAreaContactosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAreaContactoGet(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnGetAreaContactoById(ref IQueryable<SGPA.Server.Models.CMU.AreaContacto> items);


        public async Task<SGPA.Server.Models.CMU.AreaContacto> GetAreaContactoById(int id)
        {
            var items = Context.AreaContactos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetAreaContactoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAreaContactoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAreaContactoCreated(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnAfterAreaContactoCreated(SGPA.Server.Models.CMU.AreaContacto item);

        public async Task<SGPA.Server.Models.CMU.AreaContacto> CreateAreaContacto(SGPA.Server.Models.CMU.AreaContacto areacontacto)
        {
            OnAreaContactoCreated(areacontacto);

            var existingItem = Context.AreaContactos
                              .Where(i => i.Id == areacontacto.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AreaContactos.Add(areacontacto);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(areacontacto).State = EntityState.Detached;
                throw;
            }

            OnAfterAreaContactoCreated(areacontacto);

            return areacontacto;
        }

        public async Task<SGPA.Server.Models.CMU.AreaContacto> CancelAreaContactoChanges(SGPA.Server.Models.CMU.AreaContacto item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAreaContactoUpdated(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnAfterAreaContactoUpdated(SGPA.Server.Models.CMU.AreaContacto item);

        public async Task<SGPA.Server.Models.CMU.AreaContacto> UpdateAreaContacto(int id, SGPA.Server.Models.CMU.AreaContacto areacontacto)
        {
            OnAreaContactoUpdated(areacontacto);

            var itemToUpdate = Context.AreaContactos
                              .Where(i => i.Id == areacontacto.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(areacontacto);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAreaContactoUpdated(areacontacto);

            return areacontacto;
        }

        partial void OnAreaContactoDeleted(SGPA.Server.Models.CMU.AreaContacto item);
        partial void OnAfterAreaContactoDeleted(SGPA.Server.Models.CMU.AreaContacto item);

        public async Task<SGPA.Server.Models.CMU.AreaContacto> DeleteAreaContacto(int id)
        {
            var itemToDelete = Context.AreaContactos
                              .Where(i => i.Id == id)
                              .Include(i => i.Contactos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAreaContactoDeleted(itemToDelete);


            Context.AreaContactos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAreaContactoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAuditDataItemPersistentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditdataitempersistents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditdataitempersistents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAuditDataItemPersistentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditdataitempersistents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditdataitempersistents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAuditDataItemPersistentsRead(ref IQueryable<SGPA.Server.Models.CMU.AuditDataItemPersistent> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AuditDataItemPersistent>> GetAuditDataItemPersistents(Query query = null)
        {
            var items = Context.AuditDataItemPersistents.AsQueryable();

            items = items.Include(i => i.AuditedObjectWeakReference);

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

            OnAuditDataItemPersistentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAuditDataItemPersistentGet(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnGetAuditDataItemPersistentByOid(ref IQueryable<SGPA.Server.Models.CMU.AuditDataItemPersistent> items);


        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> GetAuditDataItemPersistentByOid(Guid oid)
        {
            var items = Context.AuditDataItemPersistents
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.AuditedObjectWeakReference);
 
            OnGetAuditDataItemPersistentByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAuditDataItemPersistentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAuditDataItemPersistentCreated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnAfterAuditDataItemPersistentCreated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);

        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> CreateAuditDataItemPersistent(SGPA.Server.Models.CMU.AuditDataItemPersistent auditdataitempersistent)
        {
            OnAuditDataItemPersistentCreated(auditdataitempersistent);

            var existingItem = Context.AuditDataItemPersistents
                              .Where(i => i.Oid == auditdataitempersistent.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AuditDataItemPersistents.Add(auditdataitempersistent);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(auditdataitempersistent).State = EntityState.Detached;
                throw;
            }

            OnAfterAuditDataItemPersistentCreated(auditdataitempersistent);

            return auditdataitempersistent;
        }

        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> CancelAuditDataItemPersistentChanges(SGPA.Server.Models.CMU.AuditDataItemPersistent item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAuditDataItemPersistentUpdated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnAfterAuditDataItemPersistentUpdated(SGPA.Server.Models.CMU.AuditDataItemPersistent item);

        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> UpdateAuditDataItemPersistent(Guid oid, SGPA.Server.Models.CMU.AuditDataItemPersistent auditdataitempersistent)
        {
            OnAuditDataItemPersistentUpdated(auditdataitempersistent);

            var itemToUpdate = Context.AuditDataItemPersistents
                              .Where(i => i.Oid == auditdataitempersistent.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(auditdataitempersistent);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAuditDataItemPersistentUpdated(auditdataitempersistent);

            return auditdataitempersistent;
        }

        partial void OnAuditDataItemPersistentDeleted(SGPA.Server.Models.CMU.AuditDataItemPersistent item);
        partial void OnAfterAuditDataItemPersistentDeleted(SGPA.Server.Models.CMU.AuditDataItemPersistent item);

        public async Task<SGPA.Server.Models.CMU.AuditDataItemPersistent> DeleteAuditDataItemPersistent(Guid oid)
        {
            var itemToDelete = Context.AuditDataItemPersistents
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAuditDataItemPersistentDeleted(itemToDelete);


            Context.AuditDataItemPersistents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAuditDataItemPersistentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportAuditedObjectWeakReferencesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditedobjectweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditedobjectweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAuditedObjectWeakReferencesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/auditedobjectweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/auditedobjectweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAuditedObjectWeakReferencesRead(ref IQueryable<SGPA.Server.Models.CMU.AuditedObjectWeakReference> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.AuditedObjectWeakReference>> GetAuditedObjectWeakReferences(Query query = null)
        {
            var items = Context.AuditedObjectWeakReferences.AsQueryable();


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

            OnAuditedObjectWeakReferencesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAuditedObjectWeakReferenceGet(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnGetAuditedObjectWeakReferenceByOid(ref IQueryable<SGPA.Server.Models.CMU.AuditedObjectWeakReference> items);


        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> GetAuditedObjectWeakReferenceByOid(Guid oid)
        {
            var items = Context.AuditedObjectWeakReferences
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetAuditedObjectWeakReferenceByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAuditedObjectWeakReferenceGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAuditedObjectWeakReferenceCreated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnAfterAuditedObjectWeakReferenceCreated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);

        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> CreateAuditedObjectWeakReference(SGPA.Server.Models.CMU.AuditedObjectWeakReference auditedobjectweakreference)
        {
            OnAuditedObjectWeakReferenceCreated(auditedobjectweakreference);

            var existingItem = Context.AuditedObjectWeakReferences
                              .Where(i => i.Oid == auditedobjectweakreference.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AuditedObjectWeakReferences.Add(auditedobjectweakreference);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(auditedobjectweakreference).State = EntityState.Detached;
                throw;
            }

            OnAfterAuditedObjectWeakReferenceCreated(auditedobjectweakreference);

            return auditedobjectweakreference;
        }

        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> CancelAuditedObjectWeakReferenceChanges(SGPA.Server.Models.CMU.AuditedObjectWeakReference item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAuditedObjectWeakReferenceUpdated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnAfterAuditedObjectWeakReferenceUpdated(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);

        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> UpdateAuditedObjectWeakReference(Guid oid, SGPA.Server.Models.CMU.AuditedObjectWeakReference auditedobjectweakreference)
        {
            OnAuditedObjectWeakReferenceUpdated(auditedobjectweakreference);

            var itemToUpdate = Context.AuditedObjectWeakReferences
                              .Where(i => i.Oid == auditedobjectweakreference.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(auditedobjectweakreference);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAuditedObjectWeakReferenceUpdated(auditedobjectweakreference);

            return auditedobjectweakreference;
        }

        partial void OnAuditedObjectWeakReferenceDeleted(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);
        partial void OnAfterAuditedObjectWeakReferenceDeleted(SGPA.Server.Models.CMU.AuditedObjectWeakReference item);

        public async Task<SGPA.Server.Models.CMU.AuditedObjectWeakReference> DeleteAuditedObjectWeakReference(Guid oid)
        {
            var itemToDelete = Context.AuditedObjectWeakReferences
                              .Where(i => i.Oid == oid)
                              .Include(i => i.AuditDataItemPersistents)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAuditedObjectWeakReferenceDeleted(itemToDelete);


            Context.AuditedObjectWeakReferences.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAuditedObjectWeakReferenceDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBajaMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajamotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBajaMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajamotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBajaMotivosRead(ref IQueryable<SGPA.Server.Models.CMU.BajaMotivo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.BajaMotivo>> GetBajaMotivos(Query query = null)
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

        partial void OnBajaMotivoGet(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnGetBajaMotivoById(ref IQueryable<SGPA.Server.Models.CMU.BajaMotivo> items);


        public async Task<SGPA.Server.Models.CMU.BajaMotivo> GetBajaMotivoById(int id)
        {
            var items = Context.BajaMotivos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetBajaMotivoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBajaMotivoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBajaMotivoCreated(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnAfterBajaMotivoCreated(SGPA.Server.Models.CMU.BajaMotivo item);

        public async Task<SGPA.Server.Models.CMU.BajaMotivo> CreateBajaMotivo(SGPA.Server.Models.CMU.BajaMotivo bajamotivo)
        {
            OnBajaMotivoCreated(bajamotivo);

            var existingItem = Context.BajaMotivos
                              .Where(i => i.Id == bajamotivo.Id)
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

        public async Task<SGPA.Server.Models.CMU.BajaMotivo> CancelBajaMotivoChanges(SGPA.Server.Models.CMU.BajaMotivo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBajaMotivoUpdated(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnAfterBajaMotivoUpdated(SGPA.Server.Models.CMU.BajaMotivo item);

        public async Task<SGPA.Server.Models.CMU.BajaMotivo> UpdateBajaMotivo(int id, SGPA.Server.Models.CMU.BajaMotivo bajamotivo)
        {
            OnBajaMotivoUpdated(bajamotivo);

            var itemToUpdate = Context.BajaMotivos
                              .Where(i => i.Id == bajamotivo.Id)
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

        partial void OnBajaMotivoDeleted(SGPA.Server.Models.CMU.BajaMotivo item);
        partial void OnAfterBajaMotivoDeleted(SGPA.Server.Models.CMU.BajaMotivo item);

        public async Task<SGPA.Server.Models.CMU.BajaMotivo> DeleteBajaMotivo(int id)
        {
            var itemToDelete = Context.BajaMotivos
                              .Where(i => i.Id == id)
                              .Include(i => i.Colegiados)
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
    
        public async Task ExportBajaTemporalMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajatemporalmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajatemporalmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBajaTemporalMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bajatemporalmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bajatemporalmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBajaTemporalMotivosRead(ref IQueryable<SGPA.Server.Models.CMU.BajaTemporalMotivo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.BajaTemporalMotivo>> GetBajaTemporalMotivos(Query query = null)
        {
            var items = Context.BajaTemporalMotivos.AsQueryable();


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

            OnBajaTemporalMotivosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnBajaTemporalMotivoGet(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnGetBajaTemporalMotivoById(ref IQueryable<SGPA.Server.Models.CMU.BajaTemporalMotivo> items);


        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> GetBajaTemporalMotivoById(int id)
        {
            var items = Context.BajaTemporalMotivos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetBajaTemporalMotivoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBajaTemporalMotivoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBajaTemporalMotivoCreated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnAfterBajaTemporalMotivoCreated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);

        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> CreateBajaTemporalMotivo(SGPA.Server.Models.CMU.BajaTemporalMotivo bajatemporalmotivo)
        {
            OnBajaTemporalMotivoCreated(bajatemporalmotivo);

            var existingItem = Context.BajaTemporalMotivos
                              .Where(i => i.Id == bajatemporalmotivo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.BajaTemporalMotivos.Add(bajatemporalmotivo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(bajatemporalmotivo).State = EntityState.Detached;
                throw;
            }

            OnAfterBajaTemporalMotivoCreated(bajatemporalmotivo);

            return bajatemporalmotivo;
        }

        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> CancelBajaTemporalMotivoChanges(SGPA.Server.Models.CMU.BajaTemporalMotivo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBajaTemporalMotivoUpdated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnAfterBajaTemporalMotivoUpdated(SGPA.Server.Models.CMU.BajaTemporalMotivo item);

        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> UpdateBajaTemporalMotivo(int id, SGPA.Server.Models.CMU.BajaTemporalMotivo bajatemporalmotivo)
        {
            OnBajaTemporalMotivoUpdated(bajatemporalmotivo);

            var itemToUpdate = Context.BajaTemporalMotivos
                              .Where(i => i.Id == bajatemporalmotivo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(bajatemporalmotivo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterBajaTemporalMotivoUpdated(bajatemporalmotivo);

            return bajatemporalmotivo;
        }

        partial void OnBajaTemporalMotivoDeleted(SGPA.Server.Models.CMU.BajaTemporalMotivo item);
        partial void OnAfterBajaTemporalMotivoDeleted(SGPA.Server.Models.CMU.BajaTemporalMotivo item);

        public async Task<SGPA.Server.Models.CMU.BajaTemporalMotivo> DeleteBajaTemporalMotivo(int id)
        {
            var itemToDelete = Context.BajaTemporalMotivos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnBajaTemporalMotivoDeleted(itemToDelete);


            Context.BajaTemporalMotivos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterBajaTemporalMotivoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportBancosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bancos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportBancosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/bancos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnBancosRead(ref IQueryable<SGPA.Server.Models.CMU.Banco> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Banco>> GetBancos(Query query = null)
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

        partial void OnBancoGet(SGPA.Server.Models.CMU.Banco item);
        partial void OnGetBancoById(ref IQueryable<SGPA.Server.Models.CMU.Banco> items);


        public async Task<SGPA.Server.Models.CMU.Banco> GetBancoById(int id)
        {
            var items = Context.Bancos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetBancoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnBancoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnBancoCreated(SGPA.Server.Models.CMU.Banco item);
        partial void OnAfterBancoCreated(SGPA.Server.Models.CMU.Banco item);

        public async Task<SGPA.Server.Models.CMU.Banco> CreateBanco(SGPA.Server.Models.CMU.Banco banco)
        {
            OnBancoCreated(banco);

            var existingItem = Context.Bancos
                              .Where(i => i.Id == banco.Id)
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

        public async Task<SGPA.Server.Models.CMU.Banco> CancelBancoChanges(SGPA.Server.Models.CMU.Banco item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnBancoUpdated(SGPA.Server.Models.CMU.Banco item);
        partial void OnAfterBancoUpdated(SGPA.Server.Models.CMU.Banco item);

        public async Task<SGPA.Server.Models.CMU.Banco> UpdateBanco(int id, SGPA.Server.Models.CMU.Banco banco)
        {
            OnBancoUpdated(banco);

            var itemToUpdate = Context.Bancos
                              .Where(i => i.Id == banco.Id)
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

        partial void OnBancoDeleted(SGPA.Server.Models.CMU.Banco item);
        partial void OnAfterBancoDeleted(SGPA.Server.Models.CMU.Banco item);

        public async Task<SGPA.Server.Models.CMU.Banco> DeleteBanco(int id)
        {
            var itemToDelete = Context.Bancos
                              .Where(i => i.Id == id)
                              .Include(i => i.CuentaBancaria)
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
    
        public async Task ExportCargoContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cargocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cargocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCargoContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cargocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cargocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCargoContactosRead(ref IQueryable<SGPA.Server.Models.CMU.CargoContacto> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CargoContacto>> GetCargoContactos(Query query = null)
        {
            var items = Context.CargoContactos.AsQueryable();


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

            OnCargoContactosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCargoContactoGet(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnGetCargoContactoById(ref IQueryable<SGPA.Server.Models.CMU.CargoContacto> items);


        public async Task<SGPA.Server.Models.CMU.CargoContacto> GetCargoContactoById(int id)
        {
            var items = Context.CargoContactos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetCargoContactoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCargoContactoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCargoContactoCreated(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnAfterCargoContactoCreated(SGPA.Server.Models.CMU.CargoContacto item);

        public async Task<SGPA.Server.Models.CMU.CargoContacto> CreateCargoContacto(SGPA.Server.Models.CMU.CargoContacto cargocontacto)
        {
            OnCargoContactoCreated(cargocontacto);

            var existingItem = Context.CargoContactos
                              .Where(i => i.Id == cargocontacto.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CargoContactos.Add(cargocontacto);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cargocontacto).State = EntityState.Detached;
                throw;
            }

            OnAfterCargoContactoCreated(cargocontacto);

            return cargocontacto;
        }

        public async Task<SGPA.Server.Models.CMU.CargoContacto> CancelCargoContactoChanges(SGPA.Server.Models.CMU.CargoContacto item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCargoContactoUpdated(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnAfterCargoContactoUpdated(SGPA.Server.Models.CMU.CargoContacto item);

        public async Task<SGPA.Server.Models.CMU.CargoContacto> UpdateCargoContacto(int id, SGPA.Server.Models.CMU.CargoContacto cargocontacto)
        {
            OnCargoContactoUpdated(cargocontacto);

            var itemToUpdate = Context.CargoContactos
                              .Where(i => i.Id == cargocontacto.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cargocontacto);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCargoContactoUpdated(cargocontacto);

            return cargocontacto;
        }

        partial void OnCargoContactoDeleted(SGPA.Server.Models.CMU.CargoContacto item);
        partial void OnAfterCargoContactoDeleted(SGPA.Server.Models.CMU.CargoContacto item);

        public async Task<SGPA.Server.Models.CMU.CargoContacto> DeleteCargoContacto(int id)
        {
            var itemToDelete = Context.CargoContactos
                              .Where(i => i.Id == id)
                              .Include(i => i.Contactos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCargoContactoDeleted(itemToDelete);


            Context.CargoContactos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCargoContactoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCategoriaColegiadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoriaColegiadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoriaColegiadosRead(ref IQueryable<SGPA.Server.Models.CMU.CategoriaColegiado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CategoriaColegiado>> GetCategoriaColegiados(Query query = null)
        {
            var items = Context.CategoriaColegiados.AsQueryable();

            items = items.Include(i => i.CategoriaColegiado1);

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

            OnCategoriaColegiadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoriaColegiadoGet(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnGetCategoriaColegiadoById(ref IQueryable<SGPA.Server.Models.CMU.CategoriaColegiado> items);


        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> GetCategoriaColegiadoById(int id)
        {
            var items = Context.CategoriaColegiados
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CategoriaColegiado1);
 
            OnGetCategoriaColegiadoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCategoriaColegiadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCategoriaColegiadoCreated(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnAfterCategoriaColegiadoCreated(SGPA.Server.Models.CMU.CategoriaColegiado item);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> CreateCategoriaColegiado(SGPA.Server.Models.CMU.CategoriaColegiado categoriacolegiado)
        {
            OnCategoriaColegiadoCreated(categoriacolegiado);

            var existingItem = Context.CategoriaColegiados
                              .Where(i => i.Id == categoriacolegiado.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CategoriaColegiados.Add(categoriacolegiado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(categoriacolegiado).State = EntityState.Detached;
                throw;
            }

            OnAfterCategoriaColegiadoCreated(categoriacolegiado);

            return categoriacolegiado;
        }

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> CancelCategoriaColegiadoChanges(SGPA.Server.Models.CMU.CategoriaColegiado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCategoriaColegiadoUpdated(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnAfterCategoriaColegiadoUpdated(SGPA.Server.Models.CMU.CategoriaColegiado item);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> UpdateCategoriaColegiado(int id, SGPA.Server.Models.CMU.CategoriaColegiado categoriacolegiado)
        {
            OnCategoriaColegiadoUpdated(categoriacolegiado);

            var itemToUpdate = Context.CategoriaColegiados
                              .Where(i => i.Id == categoriacolegiado.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(categoriacolegiado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCategoriaColegiadoUpdated(categoriacolegiado);

            return categoriacolegiado;
        }

        partial void OnCategoriaColegiadoDeleted(SGPA.Server.Models.CMU.CategoriaColegiado item);
        partial void OnAfterCategoriaColegiadoDeleted(SGPA.Server.Models.CMU.CategoriaColegiado item);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiado> DeleteCategoriaColegiado(int id)
        {
            var itemToDelete = Context.CategoriaColegiados
                              .Where(i => i.Id == id)
                              .Include(i => i.CategoriaColegiados1)
                              .Include(i => i.CategoriaColegiadoValors)
                              .Include(i => i.Colegiados)
                              .Include(i => i.ColegiadoCambioCategoria)
                              .Include(i => i.DeclaracionJuradaTipos)
                              .Include(i => i.MovimientoCuentaCuota)
                              .Include(i => i.Parametros)
                              .Include(i => i.Parametros1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoriaColegiadoDeleted(itemToDelete);


            Context.CategoriaColegiados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCategoriaColegiadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCategoriaColegiadoValorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiadovalors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiadovalors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoriaColegiadoValorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/categoriacolegiadovalors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/categoriacolegiadovalors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoriaColegiadoValorsRead(ref IQueryable<SGPA.Server.Models.CMU.CategoriaColegiadoValor> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CategoriaColegiadoValor>> GetCategoriaColegiadoValors(Query query = null)
        {
            var items = Context.CategoriaColegiadoValors.AsQueryable();

            items = items.Include(i => i.CategoriaColegiado1);

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

            OnCategoriaColegiadoValorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoriaColegiadoValorGet(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnGetCategoriaColegiadoValorById(ref IQueryable<SGPA.Server.Models.CMU.CategoriaColegiadoValor> items);


        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> GetCategoriaColegiadoValorById(int id)
        {
            var items = Context.CategoriaColegiadoValors
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CategoriaColegiado1);
 
            OnGetCategoriaColegiadoValorById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCategoriaColegiadoValorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCategoriaColegiadoValorCreated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnAfterCategoriaColegiadoValorCreated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> CreateCategoriaColegiadoValor(SGPA.Server.Models.CMU.CategoriaColegiadoValor categoriacolegiadovalor)
        {
            OnCategoriaColegiadoValorCreated(categoriacolegiadovalor);

            var existingItem = Context.CategoriaColegiadoValors
                              .Where(i => i.Id == categoriacolegiadovalor.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CategoriaColegiadoValors.Add(categoriacolegiadovalor);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(categoriacolegiadovalor).State = EntityState.Detached;
                throw;
            }

            OnAfterCategoriaColegiadoValorCreated(categoriacolegiadovalor);

            return categoriacolegiadovalor;
        }

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> CancelCategoriaColegiadoValorChanges(SGPA.Server.Models.CMU.CategoriaColegiadoValor item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCategoriaColegiadoValorUpdated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnAfterCategoriaColegiadoValorUpdated(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> UpdateCategoriaColegiadoValor(int id, SGPA.Server.Models.CMU.CategoriaColegiadoValor categoriacolegiadovalor)
        {
            OnCategoriaColegiadoValorUpdated(categoriacolegiadovalor);

            var itemToUpdate = Context.CategoriaColegiadoValors
                              .Where(i => i.Id == categoriacolegiadovalor.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(categoriacolegiadovalor);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCategoriaColegiadoValorUpdated(categoriacolegiadovalor);

            return categoriacolegiadovalor;
        }

        partial void OnCategoriaColegiadoValorDeleted(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);
        partial void OnAfterCategoriaColegiadoValorDeleted(SGPA.Server.Models.CMU.CategoriaColegiadoValor item);

        public async Task<SGPA.Server.Models.CMU.CategoriaColegiadoValor> DeleteCategoriaColegiadoValor(int id)
        {
            var itemToDelete = Context.CategoriaColegiadoValors
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoriaColegiadoValorDeleted(itemToDelete);


            Context.CategoriaColegiadoValors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCategoriaColegiadoValorDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCjpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCjpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCjpsRead(ref IQueryable<SGPA.Server.Models.CMU.Cjp> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Cjp>> GetCjps(Query query = null)
        {
            var items = Context.Cjps.AsQueryable();


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

            OnCjpsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCjpGet(SGPA.Server.Models.CMU.Cjp item);
        partial void OnGetCjpByCi(ref IQueryable<SGPA.Server.Models.CMU.Cjp> items);


        public async Task<SGPA.Server.Models.CMU.Cjp> GetCjpByCi(int ci)
        {
            var items = Context.Cjps
                              .AsNoTracking()
                              .Where(i => i.CI == ci);

 
            OnGetCjpByCi(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCjpGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCjpCreated(SGPA.Server.Models.CMU.Cjp item);
        partial void OnAfterCjpCreated(SGPA.Server.Models.CMU.Cjp item);

        public async Task<SGPA.Server.Models.CMU.Cjp> CreateCjp(SGPA.Server.Models.CMU.Cjp cjp)
        {
            OnCjpCreated(cjp);

            var existingItem = Context.Cjps
                              .Where(i => i.CI == cjp.CI)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cjps.Add(cjp);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cjp).State = EntityState.Detached;
                throw;
            }

            OnAfterCjpCreated(cjp);

            return cjp;
        }

        public async Task<SGPA.Server.Models.CMU.Cjp> CancelCjpChanges(SGPA.Server.Models.CMU.Cjp item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCjpUpdated(SGPA.Server.Models.CMU.Cjp item);
        partial void OnAfterCjpUpdated(SGPA.Server.Models.CMU.Cjp item);

        public async Task<SGPA.Server.Models.CMU.Cjp> UpdateCjp(int ci, SGPA.Server.Models.CMU.Cjp cjp)
        {
            OnCjpUpdated(cjp);

            var itemToUpdate = Context.Cjps
                              .Where(i => i.CI == cjp.CI)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cjp);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCjpUpdated(cjp);

            return cjp;
        }

        partial void OnCjpDeleted(SGPA.Server.Models.CMU.Cjp item);
        partial void OnAfterCjpDeleted(SGPA.Server.Models.CMU.Cjp item);

        public async Task<SGPA.Server.Models.CMU.Cjp> DeleteCjp(int ci)
        {
            var itemToDelete = Context.Cjps
                              .Where(i => i.CI == ci)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCjpDeleted(itemToDelete);


            Context.Cjps.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCjpDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCjpMatsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpmats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpmats/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCjpMatsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpmats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpmats/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCjpMatsRead(ref IQueryable<SGPA.Server.Models.CMU.CjpMat> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CjpMat>> GetCjpMats(Query query = null)
        {
            var items = Context.CjpMats.AsQueryable();


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

            OnCjpMatsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCjpMatGet(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnGetCjpMatByDocumento(ref IQueryable<SGPA.Server.Models.CMU.CjpMat> items);


        public async Task<SGPA.Server.Models.CMU.CjpMat> GetCjpMatByDocumento(int documento)
        {
            var items = Context.CjpMats
                              .AsNoTracking()
                              .Where(i => i.Documento == documento);

 
            OnGetCjpMatByDocumento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCjpMatGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCjpMatCreated(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnAfterCjpMatCreated(SGPA.Server.Models.CMU.CjpMat item);

        public async Task<SGPA.Server.Models.CMU.CjpMat> CreateCjpMat(SGPA.Server.Models.CMU.CjpMat cjpmat)
        {
            OnCjpMatCreated(cjpmat);

            var existingItem = Context.CjpMats
                              .Where(i => i.Documento == cjpmat.Documento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CjpMats.Add(cjpmat);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cjpmat).State = EntityState.Detached;
                throw;
            }

            OnAfterCjpMatCreated(cjpmat);

            return cjpmat;
        }

        public async Task<SGPA.Server.Models.CMU.CjpMat> CancelCjpMatChanges(SGPA.Server.Models.CMU.CjpMat item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCjpMatUpdated(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnAfterCjpMatUpdated(SGPA.Server.Models.CMU.CjpMat item);

        public async Task<SGPA.Server.Models.CMU.CjpMat> UpdateCjpMat(int documento, SGPA.Server.Models.CMU.CjpMat cjpmat)
        {
            OnCjpMatUpdated(cjpmat);

            var itemToUpdate = Context.CjpMats
                              .Where(i => i.Documento == cjpmat.Documento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cjpmat);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCjpMatUpdated(cjpmat);

            return cjpmat;
        }

        partial void OnCjpMatDeleted(SGPA.Server.Models.CMU.CjpMat item);
        partial void OnAfterCjpMatDeleted(SGPA.Server.Models.CMU.CjpMat item);

        public async Task<SGPA.Server.Models.CMU.CjpMat> DeleteCjpMat(int documento)
        {
            var itemToDelete = Context.CjpMats
                              .Where(i => i.Documento == documento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCjpMatDeleted(itemToDelete);


            Context.CjpMats.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCjpMatDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCjpOldsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpolds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpolds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCjpOldsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cjpolds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cjpolds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCjpOldsRead(ref IQueryable<SGPA.Server.Models.CMU.CjpOld> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CjpOld>> GetCjpOlds(Query query = null)
        {
            var items = Context.CjpOlds.AsQueryable();


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

            OnCjpOldsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCjpOldGet(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnGetCjpOldByDocumento(ref IQueryable<SGPA.Server.Models.CMU.CjpOld> items);


        public async Task<SGPA.Server.Models.CMU.CjpOld> GetCjpOldByDocumento(int documento)
        {
            var items = Context.CjpOlds
                              .AsNoTracking()
                              .Where(i => i.Documento == documento);

 
            OnGetCjpOldByDocumento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCjpOldGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCjpOldCreated(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnAfterCjpOldCreated(SGPA.Server.Models.CMU.CjpOld item);

        public async Task<SGPA.Server.Models.CMU.CjpOld> CreateCjpOld(SGPA.Server.Models.CMU.CjpOld cjpold)
        {
            OnCjpOldCreated(cjpold);

            var existingItem = Context.CjpOlds
                              .Where(i => i.Documento == cjpold.Documento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CjpOlds.Add(cjpold);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cjpold).State = EntityState.Detached;
                throw;
            }

            OnAfterCjpOldCreated(cjpold);

            return cjpold;
        }

        public async Task<SGPA.Server.Models.CMU.CjpOld> CancelCjpOldChanges(SGPA.Server.Models.CMU.CjpOld item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCjpOldUpdated(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnAfterCjpOldUpdated(SGPA.Server.Models.CMU.CjpOld item);

        public async Task<SGPA.Server.Models.CMU.CjpOld> UpdateCjpOld(int documento, SGPA.Server.Models.CMU.CjpOld cjpold)
        {
            OnCjpOldUpdated(cjpold);

            var itemToUpdate = Context.CjpOlds
                              .Where(i => i.Documento == cjpold.Documento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cjpold);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCjpOldUpdated(cjpold);

            return cjpold;
        }

        partial void OnCjpOldDeleted(SGPA.Server.Models.CMU.CjpOld item);
        partial void OnAfterCjpOldDeleted(SGPA.Server.Models.CMU.CjpOld item);

        public async Task<SGPA.Server.Models.CMU.CjpOld> DeleteCjpOld(int documento)
        {
            var itemToDelete = Context.CjpOlds
                              .Where(i => i.Documento == documento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCjpOldDeleted(itemToDelete);


            Context.CjpOlds.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCjpOldDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCobrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCobrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCobrosRead(ref IQueryable<SGPA.Server.Models.CMU.Cobro> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Cobro>> GetCobros(Query query = null)
        {
            var items = Context.Cobros.AsQueryable();

            items = items.Include(i => i.AgenteCobranza1);
            items = items.Include(i => i.CuentaBancarium);

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

            OnCobrosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCobroGet(SGPA.Server.Models.CMU.Cobro item);
        partial void OnGetCobroById(ref IQueryable<SGPA.Server.Models.CMU.Cobro> items);


        public async Task<SGPA.Server.Models.CMU.Cobro> GetCobroById(int id)
        {
            var items = Context.Cobros
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranza1);
            items = items.Include(i => i.CuentaBancarium);
 
            OnGetCobroById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCobroGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCobroCreated(SGPA.Server.Models.CMU.Cobro item);
        partial void OnAfterCobroCreated(SGPA.Server.Models.CMU.Cobro item);

        public async Task<SGPA.Server.Models.CMU.Cobro> CreateCobro(SGPA.Server.Models.CMU.Cobro cobro)
        {
            OnCobroCreated(cobro);

            var existingItem = Context.Cobros
                              .Where(i => i.Id == cobro.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cobros.Add(cobro);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cobro).State = EntityState.Detached;
                throw;
            }

            OnAfterCobroCreated(cobro);

            return cobro;
        }

        public async Task<SGPA.Server.Models.CMU.Cobro> CancelCobroChanges(SGPA.Server.Models.CMU.Cobro item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCobroUpdated(SGPA.Server.Models.CMU.Cobro item);
        partial void OnAfterCobroUpdated(SGPA.Server.Models.CMU.Cobro item);

        public async Task<SGPA.Server.Models.CMU.Cobro> UpdateCobro(int id, SGPA.Server.Models.CMU.Cobro cobro)
        {
            OnCobroUpdated(cobro);

            var itemToUpdate = Context.Cobros
                              .Where(i => i.Id == cobro.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cobro);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCobroUpdated(cobro);

            return cobro;
        }

        partial void OnCobroDeleted(SGPA.Server.Models.CMU.Cobro item);
        partial void OnAfterCobroDeleted(SGPA.Server.Models.CMU.Cobro item);

        public async Task<SGPA.Server.Models.CMU.Cobro> DeleteCobro(int id)
        {
            var itemToDelete = Context.Cobros
                              .Where(i => i.Id == id)
                              .Include(i => i.CobroNominas)
                              .Include(i => i.Debitos)
                              .Include(i => i.Depositos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCobroDeleted(itemToDelete);


            Context.Cobros.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCobroDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCobroNominasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobronominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobronominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCobroNominasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cobronominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cobronominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCobroNominasRead(ref IQueryable<SGPA.Server.Models.CMU.CobroNomina> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CobroNomina>> GetCobroNominas(Query query = null)
        {
            var items = Context.CobroNominas.AsQueryable();

            items = items.Include(i => i.Cobro1);
            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.Convenio1);
            items = items.Include(i => i.MovimientoCuentum);

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

            OnCobroNominasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCobroNominaGet(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnGetCobroNominaById(ref IQueryable<SGPA.Server.Models.CMU.CobroNomina> items);


        public async Task<SGPA.Server.Models.CMU.CobroNomina> GetCobroNominaById(int id)
        {
            var items = Context.CobroNominas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Cobro1);
            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.Convenio1);
            items = items.Include(i => i.MovimientoCuentum);
 
            OnGetCobroNominaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCobroNominaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCobroNominaCreated(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnAfterCobroNominaCreated(SGPA.Server.Models.CMU.CobroNomina item);

        public async Task<SGPA.Server.Models.CMU.CobroNomina> CreateCobroNomina(SGPA.Server.Models.CMU.CobroNomina cobronomina)
        {
            OnCobroNominaCreated(cobronomina);

            var existingItem = Context.CobroNominas
                              .Where(i => i.Id == cobronomina.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CobroNominas.Add(cobronomina);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cobronomina).State = EntityState.Detached;
                throw;
            }

            OnAfterCobroNominaCreated(cobronomina);

            return cobronomina;
        }

        public async Task<SGPA.Server.Models.CMU.CobroNomina> CancelCobroNominaChanges(SGPA.Server.Models.CMU.CobroNomina item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCobroNominaUpdated(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnAfterCobroNominaUpdated(SGPA.Server.Models.CMU.CobroNomina item);

        public async Task<SGPA.Server.Models.CMU.CobroNomina> UpdateCobroNomina(int id, SGPA.Server.Models.CMU.CobroNomina cobronomina)
        {
            OnCobroNominaUpdated(cobronomina);

            var itemToUpdate = Context.CobroNominas
                              .Where(i => i.Id == cobronomina.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cobronomina);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCobroNominaUpdated(cobronomina);

            return cobronomina;
        }

        partial void OnCobroNominaDeleted(SGPA.Server.Models.CMU.CobroNomina item);
        partial void OnAfterCobroNominaDeleted(SGPA.Server.Models.CMU.CobroNomina item);

        public async Task<SGPA.Server.Models.CMU.CobroNomina> DeleteCobroNomina(int id)
        {
            var itemToDelete = Context.CobroNominas
                              .Where(i => i.Id == id)
                              .Include(i => i.DebitoNominas)
                              .Include(i => i.DepositoNominas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCobroNominaDeleted(itemToDelete);


            Context.CobroNominas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCobroNominaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadosRead(ref IQueryable<SGPA.Server.Models.CMU.Colegiado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Colegiado>> GetColegiados(Query query = null)
        {
            var items = Context.Colegiados.AsQueryable();

            items = items.Include(i => i.AgenteCobranza);
            items = items.Include(i => i.BajaMotivo1);
            items = items.Include(i => i.CategoriaColegiado1);
            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.Pai);
            items = items.Include(i => i.Regional);
            items = items.Include(i => i.AgenteCobranza1);
            items = items.Include(i => i.UniversidadTituloGrado1);

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

            OnColegiadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoGet(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnGetColegiadoByDocumento(ref IQueryable<SGPA.Server.Models.CMU.Colegiado> items);


        public async Task<SGPA.Server.Models.CMU.Colegiado> GetColegiadoByDocumento(int documento)
        {
            var items = Context.Colegiados
                              .AsNoTracking()
                              .Where(i => i.Documento == documento);

            items = items.Include(i => i.AgenteCobranza);
            items = items.Include(i => i.BajaMotivo1);
            items = items.Include(i => i.CategoriaColegiado1);
            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.Pai);
            items = items.Include(i => i.Regional);
            items = items.Include(i => i.AgenteCobranza1);
            items = items.Include(i => i.UniversidadTituloGrado1);
 
            OnGetColegiadoByDocumento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoCreated(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnAfterColegiadoCreated(SGPA.Server.Models.CMU.Colegiado item);

        public async Task<SGPA.Server.Models.CMU.Colegiado> CreateColegiado(SGPA.Server.Models.CMU.Colegiado colegiado)
        {
            OnColegiadoCreated(colegiado);

            var existingItem = Context.Colegiados
                              .Where(i => i.Documento == colegiado.Documento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Colegiados.Add(colegiado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiado).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoCreated(colegiado);

            return colegiado;
        }

        public async Task<SGPA.Server.Models.CMU.Colegiado> CancelColegiadoChanges(SGPA.Server.Models.CMU.Colegiado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoUpdated(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnAfterColegiadoUpdated(SGPA.Server.Models.CMU.Colegiado item);

        public async Task<SGPA.Server.Models.CMU.Colegiado> UpdateColegiado(int documento, SGPA.Server.Models.CMU.Colegiado colegiado)
        {
            OnColegiadoUpdated(colegiado);

            var itemToUpdate = Context.Colegiados
                              .Where(i => i.Documento == colegiado.Documento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoUpdated(colegiado);

            return colegiado;
        }

        partial void OnColegiadoDeleted(SGPA.Server.Models.CMU.Colegiado item);
        partial void OnAfterColegiadoDeleted(SGPA.Server.Models.CMU.Colegiado item);

        public async Task<SGPA.Server.Models.CMU.Colegiado> DeleteColegiado(int documento)
        {
            var itemToDelete = Context.Colegiados
                              .Where(i => i.Documento == documento)
                              .Include(i => i.AjusteRetroactivos)
                              .Include(i => i.CobroNominas)
                              .Include(i => i.ColegiadoActualizacionDps)
                              .Include(i => i.ColegiadoBitacoras)
                              .Include(i => i.ColegiadoCambioCategoria)
                              .Include(i => i.ColegiadoCertificadoExpedidos)
                              .Include(i => i.ColegiadoDebitoBancarioAsociados)
                              .Include(i => i.ColegiadoDeclaracionJurada)
                              .Include(i => i.ColegiadoImagenes)
                              .Include(i => i.ColegiadoTarjetaDebitoAsociada)
                              .Include(i => i.Convenios)
                              .Include(i => i.MovimientoCuenta)
                              .Include(i => i.SolicitudBajas)
                              .Include(i => i.TramiteCarnes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoDeleted(itemToDelete);


            Context.Colegiados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoActualizacionDpsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoactualizaciondps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoactualizaciondps/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoActualizacionDpsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoactualizaciondps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoactualizaciondps/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoActualizacionDpsRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoActualizacionDp>> GetColegiadoActualizacionDps(Query query = null)
        {
            var items = Context.ColegiadoActualizacionDps.AsQueryable();

            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoActualizacionDpsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoActualizacionDpGet(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnGetColegiadoActualizacionDpById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> GetColegiadoActualizacionDpById(int id)
        {
            var items = Context.ColegiadoActualizacionDps
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoActualizacionDpById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoActualizacionDpGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoActualizacionDpCreated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnAfterColegiadoActualizacionDpCreated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> CreateColegiadoActualizacionDp(SGPA.Server.Models.CMU.ColegiadoActualizacionDp colegiadoactualizaciondp)
        {
            OnColegiadoActualizacionDpCreated(colegiadoactualizaciondp);

            var existingItem = Context.ColegiadoActualizacionDps
                              .Where(i => i.Id == colegiadoactualizaciondp.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoActualizacionDps.Add(colegiadoactualizaciondp);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadoactualizaciondp).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoActualizacionDpCreated(colegiadoactualizaciondp);

            return colegiadoactualizaciondp;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> CancelColegiadoActualizacionDpChanges(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoActualizacionDpUpdated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnAfterColegiadoActualizacionDpUpdated(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> UpdateColegiadoActualizacionDp(int id, SGPA.Server.Models.CMU.ColegiadoActualizacionDp colegiadoactualizaciondp)
        {
            OnColegiadoActualizacionDpUpdated(colegiadoactualizaciondp);

            var itemToUpdate = Context.ColegiadoActualizacionDps
                              .Where(i => i.Id == colegiadoactualizaciondp.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadoactualizaciondp);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoActualizacionDpUpdated(colegiadoactualizaciondp);

            return colegiadoactualizaciondp;
        }

        partial void OnColegiadoActualizacionDpDeleted(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);
        partial void OnAfterColegiadoActualizacionDpDeleted(SGPA.Server.Models.CMU.ColegiadoActualizacionDp item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoActualizacionDp> DeleteColegiadoActualizacionDp(int id)
        {
            var itemToDelete = Context.ColegiadoActualizacionDps
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoActualizacionDpDeleted(itemToDelete);


            Context.ColegiadoActualizacionDps.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoActualizacionDpDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoBitacorasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoras/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoras/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoBitacorasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoras/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoras/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoBitacorasRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacora> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacora>> GetColegiadoBitacoras(Query query = null)
        {
            var items = Context.ColegiadoBitacoras.AsQueryable();

            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoBitacorasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoBitacoraGet(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnGetColegiadoBitacoraById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacora> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> GetColegiadoBitacoraById(int id)
        {
            var items = Context.ColegiadoBitacoras
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoBitacoraById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoBitacoraGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoBitacoraCreated(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnAfterColegiadoBitacoraCreated(SGPA.Server.Models.CMU.ColegiadoBitacora item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> CreateColegiadoBitacora(SGPA.Server.Models.CMU.ColegiadoBitacora colegiadobitacora)
        {
            OnColegiadoBitacoraCreated(colegiadobitacora);

            var existingItem = Context.ColegiadoBitacoras
                              .Where(i => i.Id == colegiadobitacora.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoBitacoras.Add(colegiadobitacora);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadobitacora).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoBitacoraCreated(colegiadobitacora);

            return colegiadobitacora;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> CancelColegiadoBitacoraChanges(SGPA.Server.Models.CMU.ColegiadoBitacora item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoBitacoraUpdated(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnAfterColegiadoBitacoraUpdated(SGPA.Server.Models.CMU.ColegiadoBitacora item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> UpdateColegiadoBitacora(int id, SGPA.Server.Models.CMU.ColegiadoBitacora colegiadobitacora)
        {
            OnColegiadoBitacoraUpdated(colegiadobitacora);

            var itemToUpdate = Context.ColegiadoBitacoras
                              .Where(i => i.Id == colegiadobitacora.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadobitacora);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoBitacoraUpdated(colegiadobitacora);

            return colegiadobitacora;
        }

        partial void OnColegiadoBitacoraDeleted(SGPA.Server.Models.CMU.ColegiadoBitacora item);
        partial void OnAfterColegiadoBitacoraDeleted(SGPA.Server.Models.CMU.ColegiadoBitacora item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacora> DeleteColegiadoBitacora(int id)
        {
            var itemToDelete = Context.ColegiadoBitacoras
                              .Where(i => i.Id == id)
                              .Include(i => i.ColegiadoBitacoraNota)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoBitacoraDeleted(itemToDelete);


            Context.ColegiadoBitacoras.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoBitacoraDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoBitacoraEMailEnviosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoBitacoraEMailEnviosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoBitacoraEMailEnviosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio>> GetColegiadoBitacoraEMailEnvios(Query query = null)
        {
            var items = Context.ColegiadoBitacoraEMailEnvios.AsQueryable();

            items = items.Include(i => i.ColegiadoBitacoraNotum);

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

            OnColegiadoBitacoraEMailEnviosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoBitacoraEMailEnvioGet(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnGetColegiadoBitacoraEMailEnvioById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> GetColegiadoBitacoraEMailEnvioById(int id)
        {
            var items = Context.ColegiadoBitacoraEMailEnvios
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.ColegiadoBitacoraNotum);
 
            OnGetColegiadoBitacoraEMailEnvioById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoBitacoraEMailEnvioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoBitacoraEMailEnvioCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnAfterColegiadoBitacoraEMailEnvioCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> CreateColegiadoBitacoraEMailEnvio(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio colegiadobitacoraemailenvio)
        {
            OnColegiadoBitacoraEMailEnvioCreated(colegiadobitacoraemailenvio);

            var existingItem = Context.ColegiadoBitacoraEMailEnvios
                              .Where(i => i.Id == colegiadobitacoraemailenvio.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoBitacoraEMailEnvios.Add(colegiadobitacoraemailenvio);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadobitacoraemailenvio).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoBitacoraEMailEnvioCreated(colegiadobitacoraemailenvio);

            return colegiadobitacoraemailenvio;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> CancelColegiadoBitacoraEMailEnvioChanges(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoBitacoraEMailEnvioUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnAfterColegiadoBitacoraEMailEnvioUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> UpdateColegiadoBitacoraEMailEnvio(int id, SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio colegiadobitacoraemailenvio)
        {
            OnColegiadoBitacoraEMailEnvioUpdated(colegiadobitacoraemailenvio);

            var itemToUpdate = Context.ColegiadoBitacoraEMailEnvios
                              .Where(i => i.Id == colegiadobitacoraemailenvio.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadobitacoraemailenvio);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoBitacoraEMailEnvioUpdated(colegiadobitacoraemailenvio);

            return colegiadobitacoraemailenvio;
        }

        partial void OnColegiadoBitacoraEMailEnvioDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);
        partial void OnAfterColegiadoBitacoraEMailEnvioDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailEnvio> DeleteColegiadoBitacoraEMailEnvio(int id)
        {
            var itemToDelete = Context.ColegiadoBitacoraEMailEnvios
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoBitacoraEMailEnvioDeleted(itemToDelete);


            Context.ColegiadoBitacoraEMailEnvios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoBitacoraEMailEnvioDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoBitacoraEMailRecepcionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailrecepcions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailrecepcions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoBitacoraEMailRecepcionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoraemailrecepcions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoraemailrecepcions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoBitacoraEMailRecepcionsRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion>> GetColegiadoBitacoraEMailRecepcions(Query query = null)
        {
            var items = Context.ColegiadoBitacoraEMailRecepcions.AsQueryable();

            items = items.Include(i => i.ColegiadoBitacoraNotum);

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

            OnColegiadoBitacoraEMailRecepcionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoBitacoraEMailRecepcionGet(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnGetColegiadoBitacoraEMailRecepcionById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> GetColegiadoBitacoraEMailRecepcionById(int id)
        {
            var items = Context.ColegiadoBitacoraEMailRecepcions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.ColegiadoBitacoraNotum);
 
            OnGetColegiadoBitacoraEMailRecepcionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoBitacoraEMailRecepcionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoBitacoraEMailRecepcionCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnAfterColegiadoBitacoraEMailRecepcionCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> CreateColegiadoBitacoraEMailRecepcion(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion colegiadobitacoraemailrecepcion)
        {
            OnColegiadoBitacoraEMailRecepcionCreated(colegiadobitacoraemailrecepcion);

            var existingItem = Context.ColegiadoBitacoraEMailRecepcions
                              .Where(i => i.Id == colegiadobitacoraemailrecepcion.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoBitacoraEMailRecepcions.Add(colegiadobitacoraemailrecepcion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadobitacoraemailrecepcion).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoBitacoraEMailRecepcionCreated(colegiadobitacoraemailrecepcion);

            return colegiadobitacoraemailrecepcion;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> CancelColegiadoBitacoraEMailRecepcionChanges(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoBitacoraEMailRecepcionUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnAfterColegiadoBitacoraEMailRecepcionUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> UpdateColegiadoBitacoraEMailRecepcion(int id, SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion colegiadobitacoraemailrecepcion)
        {
            OnColegiadoBitacoraEMailRecepcionUpdated(colegiadobitacoraemailrecepcion);

            var itemToUpdate = Context.ColegiadoBitacoraEMailRecepcions
                              .Where(i => i.Id == colegiadobitacoraemailrecepcion.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadobitacoraemailrecepcion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoBitacoraEMailRecepcionUpdated(colegiadobitacoraemailrecepcion);

            return colegiadobitacoraemailrecepcion;
        }

        partial void OnColegiadoBitacoraEMailRecepcionDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);
        partial void OnAfterColegiadoBitacoraEMailRecepcionDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraEMailRecepcion> DeleteColegiadoBitacoraEMailRecepcion(int id)
        {
            var itemToDelete = Context.ColegiadoBitacoraEMailRecepcions
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoBitacoraEMailRecepcionDeleted(itemToDelete);


            Context.ColegiadoBitacoraEMailRecepcions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoBitacoraEMailRecepcionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoBitacoraNotaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoranota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoranota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoBitacoraNotaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadobitacoranota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadobitacoranota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoBitacoraNotaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum>> GetColegiadoBitacoraNota(Query query = null)
        {
            var items = Context.ColegiadoBitacoraNota.AsQueryable();

            items = items.Include(i => i.ColegiadoBitacora);

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

            OnColegiadoBitacoraNotaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoBitacoraNotumGet(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnGetColegiadoBitacoraNotumById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> GetColegiadoBitacoraNotumById(int id)
        {
            var items = Context.ColegiadoBitacoraNota
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.ColegiadoBitacora);
 
            OnGetColegiadoBitacoraNotumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoBitacoraNotumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoBitacoraNotumCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnAfterColegiadoBitacoraNotumCreated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> CreateColegiadoBitacoraNotum(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum colegiadobitacoranotum)
        {
            OnColegiadoBitacoraNotumCreated(colegiadobitacoranotum);

            var existingItem = Context.ColegiadoBitacoraNota
                              .Where(i => i.Id == colegiadobitacoranotum.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoBitacoraNota.Add(colegiadobitacoranotum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadobitacoranotum).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoBitacoraNotumCreated(colegiadobitacoranotum);

            return colegiadobitacoranotum;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> CancelColegiadoBitacoraNotumChanges(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoBitacoraNotumUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnAfterColegiadoBitacoraNotumUpdated(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> UpdateColegiadoBitacoraNotum(int id, SGPA.Server.Models.CMU.ColegiadoBitacoraNotum colegiadobitacoranotum)
        {
            OnColegiadoBitacoraNotumUpdated(colegiadobitacoranotum);

            var itemToUpdate = Context.ColegiadoBitacoraNota
                              .Where(i => i.Id == colegiadobitacoranotum.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadobitacoranotum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoBitacoraNotumUpdated(colegiadobitacoranotum);

            return colegiadobitacoranotum;
        }

        partial void OnColegiadoBitacoraNotumDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);
        partial void OnAfterColegiadoBitacoraNotumDeleted(SGPA.Server.Models.CMU.ColegiadoBitacoraNotum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoBitacoraNotum> DeleteColegiadoBitacoraNotum(int id)
        {
            var itemToDelete = Context.ColegiadoBitacoraNota
                              .Where(i => i.Id == id)
                              .Include(i => i.ColegiadoBitacoraEMailEnvios)
                              .Include(i => i.ColegiadoBitacoraEMailRecepcions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoBitacoraNotumDeleted(itemToDelete);


            Context.ColegiadoBitacoraNota.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoBitacoraNotumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoCambioCategoriaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocambiocategoria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocambiocategoria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoCambioCategoriaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocambiocategoria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocambiocategoria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoCambioCategoriaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium>> GetColegiadoCambioCategoria(Query query = null)
        {
            var items = Context.ColegiadoCambioCategoria.AsQueryable();

            items = items.Include(i => i.CategoriaColegiado);
            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoCambioCategoriaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoCambioCategoriumGet(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnGetColegiadoCambioCategoriumById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> GetColegiadoCambioCategoriumById(int id)
        {
            var items = Context.ColegiadoCambioCategoria
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CategoriaColegiado);
            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoCambioCategoriumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoCambioCategoriumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoCambioCategoriumCreated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnAfterColegiadoCambioCategoriumCreated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> CreateColegiadoCambioCategorium(SGPA.Server.Models.CMU.ColegiadoCambioCategorium colegiadocambiocategorium)
        {
            OnColegiadoCambioCategoriumCreated(colegiadocambiocategorium);

            var existingItem = Context.ColegiadoCambioCategoria
                              .Where(i => i.Id == colegiadocambiocategorium.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoCambioCategoria.Add(colegiadocambiocategorium);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadocambiocategorium).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoCambioCategoriumCreated(colegiadocambiocategorium);

            return colegiadocambiocategorium;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> CancelColegiadoCambioCategoriumChanges(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoCambioCategoriumUpdated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnAfterColegiadoCambioCategoriumUpdated(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> UpdateColegiadoCambioCategorium(int id, SGPA.Server.Models.CMU.ColegiadoCambioCategorium colegiadocambiocategorium)
        {
            OnColegiadoCambioCategoriumUpdated(colegiadocambiocategorium);

            var itemToUpdate = Context.ColegiadoCambioCategoria
                              .Where(i => i.Id == colegiadocambiocategorium.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadocambiocategorium);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoCambioCategoriumUpdated(colegiadocambiocategorium);

            return colegiadocambiocategorium;
        }

        partial void OnColegiadoCambioCategoriumDeleted(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);
        partial void OnAfterColegiadoCambioCategoriumDeleted(SGPA.Server.Models.CMU.ColegiadoCambioCategorium item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCambioCategorium> DeleteColegiadoCambioCategorium(int id)
        {
            var itemToDelete = Context.ColegiadoCambioCategoria
                              .Where(i => i.Id == id)
                              .Include(i => i.AjusteRetroactivos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoCambioCategoriumDeleted(itemToDelete);


            Context.ColegiadoCambioCategoria.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoCambioCategoriumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoCertificadoExpedidosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocertificadoexpedidos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocertificadoexpedidos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoCertificadoExpedidosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadocertificadoexpedidos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadocertificadoexpedidos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoCertificadoExpedidosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido>> GetColegiadoCertificadoExpedidos(Query query = null)
        {
            var items = Context.ColegiadoCertificadoExpedidos.AsQueryable();

            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoCertificadoExpedidosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoCertificadoExpedidoGet(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnGetColegiadoCertificadoExpedidoById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> GetColegiadoCertificadoExpedidoById(int id)
        {
            var items = Context.ColegiadoCertificadoExpedidos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoCertificadoExpedidoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoCertificadoExpedidoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoCertificadoExpedidoCreated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnAfterColegiadoCertificadoExpedidoCreated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> CreateColegiadoCertificadoExpedido(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido colegiadocertificadoexpedido)
        {
            OnColegiadoCertificadoExpedidoCreated(colegiadocertificadoexpedido);

            var existingItem = Context.ColegiadoCertificadoExpedidos
                              .Where(i => i.Id == colegiadocertificadoexpedido.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoCertificadoExpedidos.Add(colegiadocertificadoexpedido);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadocertificadoexpedido).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoCertificadoExpedidoCreated(colegiadocertificadoexpedido);

            return colegiadocertificadoexpedido;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> CancelColegiadoCertificadoExpedidoChanges(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoCertificadoExpedidoUpdated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnAfterColegiadoCertificadoExpedidoUpdated(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> UpdateColegiadoCertificadoExpedido(int id, SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido colegiadocertificadoexpedido)
        {
            OnColegiadoCertificadoExpedidoUpdated(colegiadocertificadoexpedido);

            var itemToUpdate = Context.ColegiadoCertificadoExpedidos
                              .Where(i => i.Id == colegiadocertificadoexpedido.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadocertificadoexpedido);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoCertificadoExpedidoUpdated(colegiadocertificadoexpedido);

            return colegiadocertificadoexpedido;
        }

        partial void OnColegiadoCertificadoExpedidoDeleted(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);
        partial void OnAfterColegiadoCertificadoExpedidoDeleted(SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoCertificadoExpedido> DeleteColegiadoCertificadoExpedido(int id)
        {
            var itemToDelete = Context.ColegiadoCertificadoExpedidos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoCertificadoExpedidoDeleted(itemToDelete);


            Context.ColegiadoCertificadoExpedidos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoCertificadoExpedidoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoDebitoBancarioAsociadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodebitobancarioasociados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodebitobancarioasociados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoDebitoBancarioAsociadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodebitobancarioasociados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodebitobancarioasociados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoDebitoBancarioAsociadosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado>> GetColegiadoDebitoBancarioAsociados(Query query = null)
        {
            var items = Context.ColegiadoDebitoBancarioAsociados.AsQueryable();

            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoDebitoBancarioAsociadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoDebitoBancarioAsociadoGet(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnGetColegiadoDebitoBancarioAsociadoById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> GetColegiadoDebitoBancarioAsociadoById(int id)
        {
            var items = Context.ColegiadoDebitoBancarioAsociados
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoDebitoBancarioAsociadoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoDebitoBancarioAsociadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoDebitoBancarioAsociadoCreated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnAfterColegiadoDebitoBancarioAsociadoCreated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> CreateColegiadoDebitoBancarioAsociado(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado colegiadodebitobancarioasociado)
        {
            OnColegiadoDebitoBancarioAsociadoCreated(colegiadodebitobancarioasociado);

            var existingItem = Context.ColegiadoDebitoBancarioAsociados
                              .Where(i => i.Id == colegiadodebitobancarioasociado.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoDebitoBancarioAsociados.Add(colegiadodebitobancarioasociado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadodebitobancarioasociado).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoDebitoBancarioAsociadoCreated(colegiadodebitobancarioasociado);

            return colegiadodebitobancarioasociado;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> CancelColegiadoDebitoBancarioAsociadoChanges(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoDebitoBancarioAsociadoUpdated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnAfterColegiadoDebitoBancarioAsociadoUpdated(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> UpdateColegiadoDebitoBancarioAsociado(int id, SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado colegiadodebitobancarioasociado)
        {
            OnColegiadoDebitoBancarioAsociadoUpdated(colegiadodebitobancarioasociado);

            var itemToUpdate = Context.ColegiadoDebitoBancarioAsociados
                              .Where(i => i.Id == colegiadodebitobancarioasociado.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadodebitobancarioasociado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoDebitoBancarioAsociadoUpdated(colegiadodebitobancarioasociado);

            return colegiadodebitobancarioasociado;
        }

        partial void OnColegiadoDebitoBancarioAsociadoDeleted(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);
        partial void OnAfterColegiadoDebitoBancarioAsociadoDeleted(SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDebitoBancarioAsociado> DeleteColegiadoDebitoBancarioAsociado(int id)
        {
            var itemToDelete = Context.ColegiadoDebitoBancarioAsociados
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoDebitoBancarioAsociadoDeleted(itemToDelete);


            Context.ColegiadoDebitoBancarioAsociados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoDebitoBancarioAsociadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoDeclaracionJuradaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodeclaracionjurada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodeclaracionjurada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoDeclaracionJuradaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadodeclaracionjurada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadodeclaracionjurada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoDeclaracionJuradaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum>> GetColegiadoDeclaracionJurada(Query query = null)
        {
            var items = Context.ColegiadoDeclaracionJurada.AsQueryable();

            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.DeclaracionJuradaTipo);

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

            OnColegiadoDeclaracionJuradaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoDeclaracionJuradumGet(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnGetColegiadoDeclaracionJuradumById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> GetColegiadoDeclaracionJuradumById(int id)
        {
            var items = Context.ColegiadoDeclaracionJurada
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.DeclaracionJuradaTipo);
 
            OnGetColegiadoDeclaracionJuradumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoDeclaracionJuradumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoDeclaracionJuradumCreated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnAfterColegiadoDeclaracionJuradumCreated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> CreateColegiadoDeclaracionJuradum(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadodeclaracionjuradum)
        {
            OnColegiadoDeclaracionJuradumCreated(colegiadodeclaracionjuradum);

            var existingItem = Context.ColegiadoDeclaracionJurada
                              .Where(i => i.Id == colegiadodeclaracionjuradum.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoDeclaracionJurada.Add(colegiadodeclaracionjuradum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadodeclaracionjuradum).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoDeclaracionJuradumCreated(colegiadodeclaracionjuradum);

            return colegiadodeclaracionjuradum;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> CancelColegiadoDeclaracionJuradumChanges(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoDeclaracionJuradumUpdated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnAfterColegiadoDeclaracionJuradumUpdated(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> UpdateColegiadoDeclaracionJuradum(int id, SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum colegiadodeclaracionjuradum)
        {
            OnColegiadoDeclaracionJuradumUpdated(colegiadodeclaracionjuradum);

            var itemToUpdate = Context.ColegiadoDeclaracionJurada
                              .Where(i => i.Id == colegiadodeclaracionjuradum.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadodeclaracionjuradum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoDeclaracionJuradumUpdated(colegiadodeclaracionjuradum);

            return colegiadodeclaracionjuradum;
        }

        partial void OnColegiadoDeclaracionJuradumDeleted(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);
        partial void OnAfterColegiadoDeclaracionJuradumDeleted(SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoDeclaracionJuradum> DeleteColegiadoDeclaracionJuradum(int id)
        {
            var itemToDelete = Context.ColegiadoDeclaracionJurada
                              .Where(i => i.Id == id)
                              .Include(i => i.AjusteRetroactivos)
                              .Include(i => i.DeclaracionJuradaAdjuntos)
                              .Include(i => i.SolicitudBajas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoDeclaracionJuradumDeleted(itemToDelete);


            Context.ColegiadoDeclaracionJurada.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoDeclaracionJuradumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoImagenesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoimagenes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoimagenes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoImagenesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadoimagenes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadoimagenes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoImagenesRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoImagene> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoImagene>> GetColegiadoImagenes(Query query = null)
        {
            var items = Context.ColegiadoImagenes.AsQueryable();

            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoImagenesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoImageneGet(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnGetColegiadoImageneById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoImagene> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> GetColegiadoImageneById(int id)
        {
            var items = Context.ColegiadoImagenes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoImageneById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoImageneGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoImageneCreated(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnAfterColegiadoImageneCreated(SGPA.Server.Models.CMU.ColegiadoImagene item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> CreateColegiadoImagene(SGPA.Server.Models.CMU.ColegiadoImagene colegiadoimagene)
        {
            OnColegiadoImageneCreated(colegiadoimagene);

            var existingItem = Context.ColegiadoImagenes
                              .Where(i => i.Id == colegiadoimagene.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoImagenes.Add(colegiadoimagene);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadoimagene).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoImageneCreated(colegiadoimagene);

            return colegiadoimagene;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> CancelColegiadoImageneChanges(SGPA.Server.Models.CMU.ColegiadoImagene item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoImageneUpdated(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnAfterColegiadoImageneUpdated(SGPA.Server.Models.CMU.ColegiadoImagene item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> UpdateColegiadoImagene(int id, SGPA.Server.Models.CMU.ColegiadoImagene colegiadoimagene)
        {
            OnColegiadoImageneUpdated(colegiadoimagene);

            var itemToUpdate = Context.ColegiadoImagenes
                              .Where(i => i.Id == colegiadoimagene.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadoimagene);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoImageneUpdated(colegiadoimagene);

            return colegiadoimagene;
        }

        partial void OnColegiadoImageneDeleted(SGPA.Server.Models.CMU.ColegiadoImagene item);
        partial void OnAfterColegiadoImageneDeleted(SGPA.Server.Models.CMU.ColegiadoImagene item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoImagene> DeleteColegiadoImagene(int id)
        {
            var itemToDelete = Context.ColegiadoImagenes
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoImageneDeleted(itemToDelete);


            Context.ColegiadoImagenes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoImageneDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoMovimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadomovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadomovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoMovimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadomovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadomovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoMovimientosRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoMovimiento> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoMovimiento>> GetColegiadoMovimientos(Query query = null)
        {
            var items = Context.ColegiadoMovimientos.AsQueryable();


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

            OnColegiadoMovimientosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoMovimientoGet(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnGetColegiadoMovimientoById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoMovimiento> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> GetColegiadoMovimientoById(int id)
        {
            var items = Context.ColegiadoMovimientos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetColegiadoMovimientoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoMovimientoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoMovimientoCreated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnAfterColegiadoMovimientoCreated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> CreateColegiadoMovimiento(SGPA.Server.Models.CMU.ColegiadoMovimiento colegiadomovimiento)
        {
            OnColegiadoMovimientoCreated(colegiadomovimiento);

            var existingItem = Context.ColegiadoMovimientos
                              .Where(i => i.Id == colegiadomovimiento.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoMovimientos.Add(colegiadomovimiento);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadomovimiento).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoMovimientoCreated(colegiadomovimiento);

            return colegiadomovimiento;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> CancelColegiadoMovimientoChanges(SGPA.Server.Models.CMU.ColegiadoMovimiento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoMovimientoUpdated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnAfterColegiadoMovimientoUpdated(SGPA.Server.Models.CMU.ColegiadoMovimiento item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> UpdateColegiadoMovimiento(int id, SGPA.Server.Models.CMU.ColegiadoMovimiento colegiadomovimiento)
        {
            OnColegiadoMovimientoUpdated(colegiadomovimiento);

            var itemToUpdate = Context.ColegiadoMovimientos
                              .Where(i => i.Id == colegiadomovimiento.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadomovimiento);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoMovimientoUpdated(colegiadomovimiento);

            return colegiadomovimiento;
        }

        partial void OnColegiadoMovimientoDeleted(SGPA.Server.Models.CMU.ColegiadoMovimiento item);
        partial void OnAfterColegiadoMovimientoDeleted(SGPA.Server.Models.CMU.ColegiadoMovimiento item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoMovimiento> DeleteColegiadoMovimiento(int id)
        {
            var itemToDelete = Context.ColegiadoMovimientos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoMovimientoDeleted(itemToDelete);


            Context.ColegiadoMovimientos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoMovimientoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiados2011SToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados2011s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados2011s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiados2011SToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiados2011s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiados2011s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiados2011SRead(ref IQueryable<SGPA.Server.Models.CMU.Colegiados2011> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Colegiados2011>> GetColegiados2011S(Query query = null)
        {
            var items = Context.Colegiados2011S.AsQueryable();


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

            OnColegiados2011SRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiados2011Get(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnGetColegiados2011ByDocumento(ref IQueryable<SGPA.Server.Models.CMU.Colegiados2011> items);


        public async Task<SGPA.Server.Models.CMU.Colegiados2011> GetColegiados2011ByDocumento(int documento)
        {
            var items = Context.Colegiados2011S
                              .AsNoTracking()
                              .Where(i => i.Documento == documento);

 
            OnGetColegiados2011ByDocumento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiados2011Get(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiados2011Created(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnAfterColegiados2011Created(SGPA.Server.Models.CMU.Colegiados2011 item);

        public async Task<SGPA.Server.Models.CMU.Colegiados2011> CreateColegiados2011(SGPA.Server.Models.CMU.Colegiados2011 colegiados2011)
        {
            OnColegiados2011Created(colegiados2011);

            var existingItem = Context.Colegiados2011S
                              .Where(i => i.Documento == colegiados2011.Documento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Colegiados2011S.Add(colegiados2011);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiados2011).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiados2011Created(colegiados2011);

            return colegiados2011;
        }

        public async Task<SGPA.Server.Models.CMU.Colegiados2011> CancelColegiados2011Changes(SGPA.Server.Models.CMU.Colegiados2011 item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiados2011Updated(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnAfterColegiados2011Updated(SGPA.Server.Models.CMU.Colegiados2011 item);

        public async Task<SGPA.Server.Models.CMU.Colegiados2011> UpdateColegiados2011(int documento, SGPA.Server.Models.CMU.Colegiados2011 colegiados2011)
        {
            OnColegiados2011Updated(colegiados2011);

            var itemToUpdate = Context.Colegiados2011S
                              .Where(i => i.Documento == colegiados2011.Documento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiados2011);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiados2011Updated(colegiados2011);

            return colegiados2011;
        }

        partial void OnColegiados2011Deleted(SGPA.Server.Models.CMU.Colegiados2011 item);
        partial void OnAfterColegiados2011Deleted(SGPA.Server.Models.CMU.Colegiados2011 item);

        public async Task<SGPA.Server.Models.CMU.Colegiados2011> DeleteColegiados2011(int documento)
        {
            var itemToDelete = Context.Colegiados2011S
                              .Where(i => i.Documento == documento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiados2011Deleted(itemToDelete);


            Context.Colegiados2011S.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiados2011Deleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportColegiadoTarjetaDebitoAsociadaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadotarjetadebitoasociada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadotarjetadebitoasociada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportColegiadoTarjetaDebitoAsociadaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/colegiadotarjetadebitoasociada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/colegiadotarjetadebitoasociada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnColegiadoTarjetaDebitoAsociadaRead(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum>> GetColegiadoTarjetaDebitoAsociada(Query query = null)
        {
            var items = Context.ColegiadoTarjetaDebitoAsociada.AsQueryable();

            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.Colegiado1);

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

            OnColegiadoTarjetaDebitoAsociadaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnColegiadoTarjetaDebitoAsociadumGet(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);
        partial void OnGetColegiadoTarjetaDebitoAsociadumById(ref IQueryable<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> items);


        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> GetColegiadoTarjetaDebitoAsociadumById(int id)
        {
            var items = Context.ColegiadoTarjetaDebitoAsociada
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.Colegiado1);
 
            OnGetColegiadoTarjetaDebitoAsociadumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnColegiadoTarjetaDebitoAsociadumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnColegiadoTarjetaDebitoAsociadumCreated(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);
        partial void OnAfterColegiadoTarjetaDebitoAsociadumCreated(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> CreateColegiadoTarjetaDebitoAsociadum(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum colegiadotarjetadebitoasociadum)
        {
            OnColegiadoTarjetaDebitoAsociadumCreated(colegiadotarjetadebitoasociadum);

            var existingItem = Context.ColegiadoTarjetaDebitoAsociada
                              .Where(i => i.Id == colegiadotarjetadebitoasociadum.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ColegiadoTarjetaDebitoAsociada.Add(colegiadotarjetadebitoasociadum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(colegiadotarjetadebitoasociadum).State = EntityState.Detached;
                throw;
            }

            OnAfterColegiadoTarjetaDebitoAsociadumCreated(colegiadotarjetadebitoasociadum);

            return colegiadotarjetadebitoasociadum;
        }

        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> CancelColegiadoTarjetaDebitoAsociadumChanges(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnColegiadoTarjetaDebitoAsociadumUpdated(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);
        partial void OnAfterColegiadoTarjetaDebitoAsociadumUpdated(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> UpdateColegiadoTarjetaDebitoAsociadum(int id, SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum colegiadotarjetadebitoasociadum)
        {
            OnColegiadoTarjetaDebitoAsociadumUpdated(colegiadotarjetadebitoasociadum);

            var itemToUpdate = Context.ColegiadoTarjetaDebitoAsociada
                              .Where(i => i.Id == colegiadotarjetadebitoasociadum.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(colegiadotarjetadebitoasociadum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterColegiadoTarjetaDebitoAsociadumUpdated(colegiadotarjetadebitoasociadum);

            return colegiadotarjetadebitoasociadum;
        }

        partial void OnColegiadoTarjetaDebitoAsociadumDeleted(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);
        partial void OnAfterColegiadoTarjetaDebitoAsociadumDeleted(SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum item);

        public async Task<SGPA.Server.Models.CMU.ColegiadoTarjetaDebitoAsociadum> DeleteColegiadoTarjetaDebitoAsociadum(int id)
        {
            var itemToDelete = Context.ColegiadoTarjetaDebitoAsociada
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnColegiadoTarjetaDebitoAsociadumDeleted(itemToDelete);


            Context.ColegiadoTarjetaDebitoAsociada.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterColegiadoTarjetaDebitoAsociadumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactosRead(ref IQueryable<SGPA.Server.Models.CMU.Contacto> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Contacto>> GetContactos(Query query = null)
        {
            var items = Context.Contactos.AsQueryable();

            items = items.Include(i => i.AreaContacto);
            items = items.Include(i => i.CargoContacto);
            items = items.Include(i => i.GrupoContacto);

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

            OnContactosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactoGet(SGPA.Server.Models.CMU.Contacto item);
        partial void OnGetContactoById(ref IQueryable<SGPA.Server.Models.CMU.Contacto> items);


        public async Task<SGPA.Server.Models.CMU.Contacto> GetContactoById(int id)
        {
            var items = Context.Contactos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AreaContacto);
            items = items.Include(i => i.CargoContacto);
            items = items.Include(i => i.GrupoContacto);
 
            OnGetContactoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactoCreated(SGPA.Server.Models.CMU.Contacto item);
        partial void OnAfterContactoCreated(SGPA.Server.Models.CMU.Contacto item);

        public async Task<SGPA.Server.Models.CMU.Contacto> CreateContacto(SGPA.Server.Models.CMU.Contacto contacto)
        {
            OnContactoCreated(contacto);

            var existingItem = Context.Contactos
                              .Where(i => i.Id == contacto.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contactos.Add(contacto);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contacto).State = EntityState.Detached;
                throw;
            }

            OnAfterContactoCreated(contacto);

            return contacto;
        }

        public async Task<SGPA.Server.Models.CMU.Contacto> CancelContactoChanges(SGPA.Server.Models.CMU.Contacto item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactoUpdated(SGPA.Server.Models.CMU.Contacto item);
        partial void OnAfterContactoUpdated(SGPA.Server.Models.CMU.Contacto item);

        public async Task<SGPA.Server.Models.CMU.Contacto> UpdateContacto(int id, SGPA.Server.Models.CMU.Contacto contacto)
        {
            OnContactoUpdated(contacto);

            var itemToUpdate = Context.Contactos
                              .Where(i => i.Id == contacto.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contacto);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactoUpdated(contacto);

            return contacto;
        }

        partial void OnContactoDeleted(SGPA.Server.Models.CMU.Contacto item);
        partial void OnAfterContactoDeleted(SGPA.Server.Models.CMU.Contacto item);

        public async Task<SGPA.Server.Models.CMU.Contacto> DeleteContacto(int id)
        {
            var itemToDelete = Context.Contactos
                              .Where(i => i.Id == id)
                              .Include(i => i.ContactoInfoAdicionals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactoDeleted(itemToDelete);


            Context.Contactos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactoInfoAdicionalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactoinfoadicionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactoinfoadicionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactoInfoAdicionalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/contactoinfoadicionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/contactoinfoadicionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactoInfoAdicionalsRead(ref IQueryable<SGPA.Server.Models.CMU.ContactoInfoAdicional> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ContactoInfoAdicional>> GetContactoInfoAdicionals(Query query = null)
        {
            var items = Context.ContactoInfoAdicionals.AsQueryable();

            items = items.Include(i => i.Contacto1);

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

            OnContactoInfoAdicionalsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactoInfoAdicionalGet(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnGetContactoInfoAdicionalById(ref IQueryable<SGPA.Server.Models.CMU.ContactoInfoAdicional> items);


        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> GetContactoInfoAdicionalById(int id)
        {
            var items = Context.ContactoInfoAdicionals
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Contacto1);
 
            OnGetContactoInfoAdicionalById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactoInfoAdicionalGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactoInfoAdicionalCreated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnAfterContactoInfoAdicionalCreated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);

        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> CreateContactoInfoAdicional(SGPA.Server.Models.CMU.ContactoInfoAdicional contactoinfoadicional)
        {
            OnContactoInfoAdicionalCreated(contactoinfoadicional);

            var existingItem = Context.ContactoInfoAdicionals
                              .Where(i => i.Id == contactoinfoadicional.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ContactoInfoAdicionals.Add(contactoinfoadicional);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contactoinfoadicional).State = EntityState.Detached;
                throw;
            }

            OnAfterContactoInfoAdicionalCreated(contactoinfoadicional);

            return contactoinfoadicional;
        }

        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> CancelContactoInfoAdicionalChanges(SGPA.Server.Models.CMU.ContactoInfoAdicional item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactoInfoAdicionalUpdated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnAfterContactoInfoAdicionalUpdated(SGPA.Server.Models.CMU.ContactoInfoAdicional item);

        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> UpdateContactoInfoAdicional(int id, SGPA.Server.Models.CMU.ContactoInfoAdicional contactoinfoadicional)
        {
            OnContactoInfoAdicionalUpdated(contactoinfoadicional);

            var itemToUpdate = Context.ContactoInfoAdicionals
                              .Where(i => i.Id == contactoinfoadicional.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contactoinfoadicional);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactoInfoAdicionalUpdated(contactoinfoadicional);

            return contactoinfoadicional;
        }

        partial void OnContactoInfoAdicionalDeleted(SGPA.Server.Models.CMU.ContactoInfoAdicional item);
        partial void OnAfterContactoInfoAdicionalDeleted(SGPA.Server.Models.CMU.ContactoInfoAdicional item);

        public async Task<SGPA.Server.Models.CMU.ContactoInfoAdicional> DeleteContactoInfoAdicional(int id)
        {
            var itemToDelete = Context.ContactoInfoAdicionals
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactoInfoAdicionalDeleted(itemToDelete);


            Context.ContactoInfoAdicionals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactoInfoAdicionalDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportConveniosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/convenios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/convenios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportConveniosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/convenios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/convenios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnConveniosRead(ref IQueryable<SGPA.Server.Models.CMU.Convenio> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Convenio>> GetConvenios(Query query = null)
        {
            var items = Context.Convenios.AsQueryable();

            items = items.Include(i => i.Colegiado1);

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

            OnConveniosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnConvenioGet(SGPA.Server.Models.CMU.Convenio item);
        partial void OnGetConvenioById(ref IQueryable<SGPA.Server.Models.CMU.Convenio> items);


        public async Task<SGPA.Server.Models.CMU.Convenio> GetConvenioById(int id)
        {
            var items = Context.Convenios
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Colegiado1);
 
            OnGetConvenioById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnConvenioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnConvenioCreated(SGPA.Server.Models.CMU.Convenio item);
        partial void OnAfterConvenioCreated(SGPA.Server.Models.CMU.Convenio item);

        public async Task<SGPA.Server.Models.CMU.Convenio> CreateConvenio(SGPA.Server.Models.CMU.Convenio convenio)
        {
            OnConvenioCreated(convenio);

            var existingItem = Context.Convenios
                              .Where(i => i.Id == convenio.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Convenios.Add(convenio);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(convenio).State = EntityState.Detached;
                throw;
            }

            OnAfterConvenioCreated(convenio);

            return convenio;
        }

        public async Task<SGPA.Server.Models.CMU.Convenio> CancelConvenioChanges(SGPA.Server.Models.CMU.Convenio item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnConvenioUpdated(SGPA.Server.Models.CMU.Convenio item);
        partial void OnAfterConvenioUpdated(SGPA.Server.Models.CMU.Convenio item);

        public async Task<SGPA.Server.Models.CMU.Convenio> UpdateConvenio(int id, SGPA.Server.Models.CMU.Convenio convenio)
        {
            OnConvenioUpdated(convenio);

            var itemToUpdate = Context.Convenios
                              .Where(i => i.Id == convenio.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(convenio);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterConvenioUpdated(convenio);

            return convenio;
        }

        partial void OnConvenioDeleted(SGPA.Server.Models.CMU.Convenio item);
        partial void OnAfterConvenioDeleted(SGPA.Server.Models.CMU.Convenio item);

        public async Task<SGPA.Server.Models.CMU.Convenio> DeleteConvenio(int id)
        {
            var itemToDelete = Context.Convenios
                              .Where(i => i.Id == id)
                              .Include(i => i.CobroNominas)
                              .Include(i => i.ConvenioFinanciacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnConvenioDeleted(itemToDelete);


            Context.Convenios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterConvenioDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportConvenioFinanciacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/conveniofinanciacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/conveniofinanciacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportConvenioFinanciacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/conveniofinanciacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/conveniofinanciacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnConvenioFinanciacionsRead(ref IQueryable<SGPA.Server.Models.CMU.ConvenioFinanciacion> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ConvenioFinanciacion>> GetConvenioFinanciacions(Query query = null)
        {
            var items = Context.ConvenioFinanciacions.AsQueryable();

            items = items.Include(i => i.Convenio);

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

            OnConvenioFinanciacionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnConvenioFinanciacionGet(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnGetConvenioFinanciacionById(ref IQueryable<SGPA.Server.Models.CMU.ConvenioFinanciacion> items);


        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> GetConvenioFinanciacionById(int id)
        {
            var items = Context.ConvenioFinanciacions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Convenio);
 
            OnGetConvenioFinanciacionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnConvenioFinanciacionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnConvenioFinanciacionCreated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnAfterConvenioFinanciacionCreated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);

        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> CreateConvenioFinanciacion(SGPA.Server.Models.CMU.ConvenioFinanciacion conveniofinanciacion)
        {
            OnConvenioFinanciacionCreated(conveniofinanciacion);

            var existingItem = Context.ConvenioFinanciacions
                              .Where(i => i.Id == conveniofinanciacion.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ConvenioFinanciacions.Add(conveniofinanciacion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(conveniofinanciacion).State = EntityState.Detached;
                throw;
            }

            OnAfterConvenioFinanciacionCreated(conveniofinanciacion);

            return conveniofinanciacion;
        }

        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> CancelConvenioFinanciacionChanges(SGPA.Server.Models.CMU.ConvenioFinanciacion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnConvenioFinanciacionUpdated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnAfterConvenioFinanciacionUpdated(SGPA.Server.Models.CMU.ConvenioFinanciacion item);

        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> UpdateConvenioFinanciacion(int id, SGPA.Server.Models.CMU.ConvenioFinanciacion conveniofinanciacion)
        {
            OnConvenioFinanciacionUpdated(conveniofinanciacion);

            var itemToUpdate = Context.ConvenioFinanciacions
                              .Where(i => i.Id == conveniofinanciacion.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(conveniofinanciacion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterConvenioFinanciacionUpdated(conveniofinanciacion);

            return conveniofinanciacion;
        }

        partial void OnConvenioFinanciacionDeleted(SGPA.Server.Models.CMU.ConvenioFinanciacion item);
        partial void OnAfterConvenioFinanciacionDeleted(SGPA.Server.Models.CMU.ConvenioFinanciacion item);

        public async Task<SGPA.Server.Models.CMU.ConvenioFinanciacion> DeleteConvenioFinanciacion(int id)
        {
            var itemToDelete = Context.ConvenioFinanciacions
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnConvenioFinanciacionDeleted(itemToDelete);


            Context.ConvenioFinanciacions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterConvenioFinanciacionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCuentaBancariaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuentaBancariaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/cuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/cuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuentaBancariaRead(ref IQueryable<SGPA.Server.Models.CMU.CuentaBancarium> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.CuentaBancarium>> GetCuentaBancaria(Query query = null)
        {
            var items = Context.CuentaBancaria.AsQueryable();

            items = items.Include(i => i.Banco1);

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

            OnCuentaBancariaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuentaBancariumGet(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnGetCuentaBancariumById(ref IQueryable<SGPA.Server.Models.CMU.CuentaBancarium> items);


        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> GetCuentaBancariumById(int id)
        {
            var items = Context.CuentaBancaria
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Banco1);
 
            OnGetCuentaBancariumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCuentaBancariumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCuentaBancariumCreated(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnAfterCuentaBancariumCreated(SGPA.Server.Models.CMU.CuentaBancarium item);

        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> CreateCuentaBancarium(SGPA.Server.Models.CMU.CuentaBancarium cuentabancarium)
        {
            OnCuentaBancariumCreated(cuentabancarium);

            var existingItem = Context.CuentaBancaria
                              .Where(i => i.Id == cuentabancarium.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CuentaBancaria.Add(cuentabancarium);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuentabancarium).State = EntityState.Detached;
                throw;
            }

            OnAfterCuentaBancariumCreated(cuentabancarium);

            return cuentabancarium;
        }

        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> CancelCuentaBancariumChanges(SGPA.Server.Models.CMU.CuentaBancarium item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuentaBancariumUpdated(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnAfterCuentaBancariumUpdated(SGPA.Server.Models.CMU.CuentaBancarium item);

        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> UpdateCuentaBancarium(int id, SGPA.Server.Models.CMU.CuentaBancarium cuentabancarium)
        {
            OnCuentaBancariumUpdated(cuentabancarium);

            var itemToUpdate = Context.CuentaBancaria
                              .Where(i => i.Id == cuentabancarium.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuentabancarium);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCuentaBancariumUpdated(cuentabancarium);

            return cuentabancarium;
        }

        partial void OnCuentaBancariumDeleted(SGPA.Server.Models.CMU.CuentaBancarium item);
        partial void OnAfterCuentaBancariumDeleted(SGPA.Server.Models.CMU.CuentaBancarium item);

        public async Task<SGPA.Server.Models.CMU.CuentaBancarium> DeleteCuentaBancarium(int id)
        {
            var itemToDelete = Context.CuentaBancaria
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzas)
                              .Include(i => i.Cobros)
                              .Include(i => i.RegionalregionalesCuentabancariacuentabancaria)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuentaBancariumDeleted(itemToDelete);


            Context.CuentaBancaria.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuentaBancariumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDebitosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDebitosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDebitosRead(ref IQueryable<SGPA.Server.Models.CMU.Debito> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Debito>> GetDebitos(Query query = null)
        {
            var items = Context.Debitos.AsQueryable();

            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.Cobro);

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

            OnDebitosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDebitoGet(SGPA.Server.Models.CMU.Debito item);
        partial void OnGetDebitoById(ref IQueryable<SGPA.Server.Models.CMU.Debito> items);


        public async Task<SGPA.Server.Models.CMU.Debito> GetDebitoById(int id)
        {
            var items = Context.Debitos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.Cobro);
 
            OnGetDebitoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDebitoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDebitoCreated(SGPA.Server.Models.CMU.Debito item);
        partial void OnAfterDebitoCreated(SGPA.Server.Models.CMU.Debito item);

        public async Task<SGPA.Server.Models.CMU.Debito> CreateDebito(SGPA.Server.Models.CMU.Debito debito)
        {
            OnDebitoCreated(debito);

            var existingItem = Context.Debitos
                              .Where(i => i.Id == debito.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Debitos.Add(debito);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(debito).State = EntityState.Detached;
                throw;
            }

            OnAfterDebitoCreated(debito);

            return debito;
        }

        public async Task<SGPA.Server.Models.CMU.Debito> CancelDebitoChanges(SGPA.Server.Models.CMU.Debito item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDebitoUpdated(SGPA.Server.Models.CMU.Debito item);
        partial void OnAfterDebitoUpdated(SGPA.Server.Models.CMU.Debito item);

        public async Task<SGPA.Server.Models.CMU.Debito> UpdateDebito(int id, SGPA.Server.Models.CMU.Debito debito)
        {
            OnDebitoUpdated(debito);

            var itemToUpdate = Context.Debitos
                              .Where(i => i.Id == debito.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(debito);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDebitoUpdated(debito);

            return debito;
        }

        partial void OnDebitoDeleted(SGPA.Server.Models.CMU.Debito item);
        partial void OnAfterDebitoDeleted(SGPA.Server.Models.CMU.Debito item);

        public async Task<SGPA.Server.Models.CMU.Debito> DeleteDebito(int id)
        {
            var itemToDelete = Context.Debitos
                              .Where(i => i.Id == id)
                              .Include(i => i.DebitoAdjuntos)
                              .Include(i => i.DebitoNominas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDebitoDeleted(itemToDelete);


            Context.Debitos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDebitoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDebitoAdjuntosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitoadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitoadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDebitoAdjuntosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitoadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitoadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDebitoAdjuntosRead(ref IQueryable<SGPA.Server.Models.CMU.DebitoAdjunto> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DebitoAdjunto>> GetDebitoAdjuntos(Query query = null)
        {
            var items = Context.DebitoAdjuntos.AsQueryable();

            items = items.Include(i => i.Debito1);
            items = items.Include(i => i.FileDatum);

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

            OnDebitoAdjuntosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDebitoAdjuntoGet(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnGetDebitoAdjuntoById(ref IQueryable<SGPA.Server.Models.CMU.DebitoAdjunto> items);


        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> GetDebitoAdjuntoById(int id)
        {
            var items = Context.DebitoAdjuntos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Debito1);
            items = items.Include(i => i.FileDatum);
 
            OnGetDebitoAdjuntoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDebitoAdjuntoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDebitoAdjuntoCreated(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnAfterDebitoAdjuntoCreated(SGPA.Server.Models.CMU.DebitoAdjunto item);

        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> CreateDebitoAdjunto(SGPA.Server.Models.CMU.DebitoAdjunto debitoadjunto)
        {
            OnDebitoAdjuntoCreated(debitoadjunto);

            var existingItem = Context.DebitoAdjuntos
                              .Where(i => i.Id == debitoadjunto.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DebitoAdjuntos.Add(debitoadjunto);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(debitoadjunto).State = EntityState.Detached;
                throw;
            }

            OnAfterDebitoAdjuntoCreated(debitoadjunto);

            return debitoadjunto;
        }

        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> CancelDebitoAdjuntoChanges(SGPA.Server.Models.CMU.DebitoAdjunto item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDebitoAdjuntoUpdated(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnAfterDebitoAdjuntoUpdated(SGPA.Server.Models.CMU.DebitoAdjunto item);

        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> UpdateDebitoAdjunto(int id, SGPA.Server.Models.CMU.DebitoAdjunto debitoadjunto)
        {
            OnDebitoAdjuntoUpdated(debitoadjunto);

            var itemToUpdate = Context.DebitoAdjuntos
                              .Where(i => i.Id == debitoadjunto.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(debitoadjunto);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDebitoAdjuntoUpdated(debitoadjunto);

            return debitoadjunto;
        }

        partial void OnDebitoAdjuntoDeleted(SGPA.Server.Models.CMU.DebitoAdjunto item);
        partial void OnAfterDebitoAdjuntoDeleted(SGPA.Server.Models.CMU.DebitoAdjunto item);

        public async Task<SGPA.Server.Models.CMU.DebitoAdjunto> DeleteDebitoAdjunto(int id)
        {
            var itemToDelete = Context.DebitoAdjuntos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDebitoAdjuntoDeleted(itemToDelete);


            Context.DebitoAdjuntos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDebitoAdjuntoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDebitoNominasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDebitoNominasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/debitonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/debitonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDebitoNominasRead(ref IQueryable<SGPA.Server.Models.CMU.DebitoNomina> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DebitoNomina>> GetDebitoNominas(Query query = null)
        {
            var items = Context.DebitoNominas.AsQueryable();

            items = items.Include(i => i.Debito1);
            items = items.Include(i => i.CobroNomina);

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

            OnDebitoNominasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDebitoNominaGet(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnGetDebitoNominaById(ref IQueryable<SGPA.Server.Models.CMU.DebitoNomina> items);


        public async Task<SGPA.Server.Models.CMU.DebitoNomina> GetDebitoNominaById(int id)
        {
            var items = Context.DebitoNominas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Debito1);
            items = items.Include(i => i.CobroNomina);
 
            OnGetDebitoNominaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDebitoNominaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDebitoNominaCreated(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnAfterDebitoNominaCreated(SGPA.Server.Models.CMU.DebitoNomina item);

        public async Task<SGPA.Server.Models.CMU.DebitoNomina> CreateDebitoNomina(SGPA.Server.Models.CMU.DebitoNomina debitonomina)
        {
            OnDebitoNominaCreated(debitonomina);

            var existingItem = Context.DebitoNominas
                              .Where(i => i.Id == debitonomina.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DebitoNominas.Add(debitonomina);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(debitonomina).State = EntityState.Detached;
                throw;
            }

            OnAfterDebitoNominaCreated(debitonomina);

            return debitonomina;
        }

        public async Task<SGPA.Server.Models.CMU.DebitoNomina> CancelDebitoNominaChanges(SGPA.Server.Models.CMU.DebitoNomina item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDebitoNominaUpdated(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnAfterDebitoNominaUpdated(SGPA.Server.Models.CMU.DebitoNomina item);

        public async Task<SGPA.Server.Models.CMU.DebitoNomina> UpdateDebitoNomina(int id, SGPA.Server.Models.CMU.DebitoNomina debitonomina)
        {
            OnDebitoNominaUpdated(debitonomina);

            var itemToUpdate = Context.DebitoNominas
                              .Where(i => i.Id == debitonomina.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(debitonomina);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDebitoNominaUpdated(debitonomina);

            return debitonomina;
        }

        partial void OnDebitoNominaDeleted(SGPA.Server.Models.CMU.DebitoNomina item);
        partial void OnAfterDebitoNominaDeleted(SGPA.Server.Models.CMU.DebitoNomina item);

        public async Task<SGPA.Server.Models.CMU.DebitoNomina> DeleteDebitoNomina(int id)
        {
            var itemToDelete = Context.DebitoNominas
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDebitoNominaDeleted(itemToDelete);


            Context.DebitoNominas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDebitoNominaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDeclaracionJuradaAdjuntosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradaadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradaadjuntos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDeclaracionJuradaAdjuntosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradaadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradaadjuntos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDeclaracionJuradaAdjuntosRead(ref IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto>> GetDeclaracionJuradaAdjuntos(Query query = null)
        {
            var items = Context.DeclaracionJuradaAdjuntos.AsQueryable();

            items = items.Include(i => i.ColegiadoDeclaracionJuradum);
            items = items.Include(i => i.FileDatum);

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

            OnDeclaracionJuradaAdjuntosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDeclaracionJuradaAdjuntoGet(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnGetDeclaracionJuradaAdjuntoById(ref IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> items);


        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> GetDeclaracionJuradaAdjuntoById(int id)
        {
            var items = Context.DeclaracionJuradaAdjuntos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.ColegiadoDeclaracionJuradum);
            items = items.Include(i => i.FileDatum);
 
            OnGetDeclaracionJuradaAdjuntoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDeclaracionJuradaAdjuntoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDeclaracionJuradaAdjuntoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnAfterDeclaracionJuradaAdjuntoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> CreateDeclaracionJuradaAdjunto(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto declaracionjuradaadjunto)
        {
            OnDeclaracionJuradaAdjuntoCreated(declaracionjuradaadjunto);

            var existingItem = Context.DeclaracionJuradaAdjuntos
                              .Where(i => i.Id == declaracionjuradaadjunto.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DeclaracionJuradaAdjuntos.Add(declaracionjuradaadjunto);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(declaracionjuradaadjunto).State = EntityState.Detached;
                throw;
            }

            OnAfterDeclaracionJuradaAdjuntoCreated(declaracionjuradaadjunto);

            return declaracionjuradaadjunto;
        }

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> CancelDeclaracionJuradaAdjuntoChanges(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDeclaracionJuradaAdjuntoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnAfterDeclaracionJuradaAdjuntoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> UpdateDeclaracionJuradaAdjunto(int id, SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto declaracionjuradaadjunto)
        {
            OnDeclaracionJuradaAdjuntoUpdated(declaracionjuradaadjunto);

            var itemToUpdate = Context.DeclaracionJuradaAdjuntos
                              .Where(i => i.Id == declaracionjuradaadjunto.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(declaracionjuradaadjunto);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDeclaracionJuradaAdjuntoUpdated(declaracionjuradaadjunto);

            return declaracionjuradaadjunto;
        }

        partial void OnDeclaracionJuradaAdjuntoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);
        partial void OnAfterDeclaracionJuradaAdjuntoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto item);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaAdjunto> DeleteDeclaracionJuradaAdjunto(int id)
        {
            var itemToDelete = Context.DeclaracionJuradaAdjuntos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDeclaracionJuradaAdjuntoDeleted(itemToDelete);


            Context.DeclaracionJuradaAdjuntos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDeclaracionJuradaAdjuntoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDeclaracionJuradaTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradatipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDeclaracionJuradaTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/declaracionjuradatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/declaracionjuradatipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDeclaracionJuradaTiposRead(ref IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo>> GetDeclaracionJuradaTipos(Query query = null)
        {
            var items = Context.DeclaracionJuradaTipos.AsQueryable();

            items = items.Include(i => i.CategoriaColegiado);

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

            OnDeclaracionJuradaTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDeclaracionJuradaTipoGet(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnGetDeclaracionJuradaTipoById(ref IQueryable<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> items);


        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> GetDeclaracionJuradaTipoById(int id)
        {
            var items = Context.DeclaracionJuradaTipos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CategoriaColegiado);
 
            OnGetDeclaracionJuradaTipoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDeclaracionJuradaTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDeclaracionJuradaTipoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnAfterDeclaracionJuradaTipoCreated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> CreateDeclaracionJuradaTipo(SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionjuradatipo)
        {
            OnDeclaracionJuradaTipoCreated(declaracionjuradatipo);

            var existingItem = Context.DeclaracionJuradaTipos
                              .Where(i => i.Id == declaracionjuradatipo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DeclaracionJuradaTipos.Add(declaracionjuradatipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(declaracionjuradatipo).State = EntityState.Detached;
                throw;
            }

            OnAfterDeclaracionJuradaTipoCreated(declaracionjuradatipo);

            return declaracionjuradatipo;
        }

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> CancelDeclaracionJuradaTipoChanges(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDeclaracionJuradaTipoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnAfterDeclaracionJuradaTipoUpdated(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> UpdateDeclaracionJuradaTipo(int id, SGPA.Server.Models.CMU.DeclaracionJuradaTipo declaracionjuradatipo)
        {
            OnDeclaracionJuradaTipoUpdated(declaracionjuradatipo);

            var itemToUpdate = Context.DeclaracionJuradaTipos
                              .Where(i => i.Id == declaracionjuradatipo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(declaracionjuradatipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDeclaracionJuradaTipoUpdated(declaracionjuradatipo);

            return declaracionjuradatipo;
        }

        partial void OnDeclaracionJuradaTipoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);
        partial void OnAfterDeclaracionJuradaTipoDeleted(SGPA.Server.Models.CMU.DeclaracionJuradaTipo item);

        public async Task<SGPA.Server.Models.CMU.DeclaracionJuradaTipo> DeleteDeclaracionJuradaTipo(int id)
        {
            var itemToDelete = Context.DeclaracionJuradaTipos
                              .Where(i => i.Id == id)
                              .Include(i => i.ColegiadoDeclaracionJurada)
                              .Include(i => i.Parametros)
                              .Include(i => i.Parametros1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDeclaracionJuradaTipoDeleted(itemToDelete);


            Context.DeclaracionJuradaTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDeclaracionJuradaTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepartamentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/departamentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepartamentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/departamentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepartamentosRead(ref IQueryable<SGPA.Server.Models.CMU.Departamento> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Departamento>> GetDepartamentos(Query query = null)
        {
            var items = Context.Departamentos.AsQueryable();

            items = items.Include(i => i.Regional1);

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

        partial void OnDepartamentoGet(SGPA.Server.Models.CMU.Departamento item);
        partial void OnGetDepartamentoById(ref IQueryable<SGPA.Server.Models.CMU.Departamento> items);


        public async Task<SGPA.Server.Models.CMU.Departamento> GetDepartamentoById(int id)
        {
            var items = Context.Departamentos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Regional1);
 
            OnGetDepartamentoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepartamentoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepartamentoCreated(SGPA.Server.Models.CMU.Departamento item);
        partial void OnAfterDepartamentoCreated(SGPA.Server.Models.CMU.Departamento item);

        public async Task<SGPA.Server.Models.CMU.Departamento> CreateDepartamento(SGPA.Server.Models.CMU.Departamento departamento)
        {
            OnDepartamentoCreated(departamento);

            var existingItem = Context.Departamentos
                              .Where(i => i.Id == departamento.Id)
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

        public async Task<SGPA.Server.Models.CMU.Departamento> CancelDepartamentoChanges(SGPA.Server.Models.CMU.Departamento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepartamentoUpdated(SGPA.Server.Models.CMU.Departamento item);
        partial void OnAfterDepartamentoUpdated(SGPA.Server.Models.CMU.Departamento item);

        public async Task<SGPA.Server.Models.CMU.Departamento> UpdateDepartamento(int id, SGPA.Server.Models.CMU.Departamento departamento)
        {
            OnDepartamentoUpdated(departamento);

            var itemToUpdate = Context.Departamentos
                              .Where(i => i.Id == departamento.Id)
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

        partial void OnDepartamentoDeleted(SGPA.Server.Models.CMU.Departamento item);
        partial void OnAfterDepartamentoDeleted(SGPA.Server.Models.CMU.Departamento item);

        public async Task<SGPA.Server.Models.CMU.Departamento> DeleteDepartamento(int id)
        {
            var itemToDelete = Context.Departamentos
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzas)
                              .Include(i => i.Colegiados)
                              .Include(i => i.LugarRetiroCarnes)
                              .Include(i => i.RegistroColegiados)
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
    
        public async Task ExportDepositosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepositosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepositosRead(ref IQueryable<SGPA.Server.Models.CMU.Deposito> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Deposito>> GetDepositos(Query query = null)
        {
            var items = Context.Depositos.AsQueryable();

            items = items.Include(i => i.Cobro);

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

            OnDepositosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepositoGet(SGPA.Server.Models.CMU.Deposito item);
        partial void OnGetDepositoById(ref IQueryable<SGPA.Server.Models.CMU.Deposito> items);


        public async Task<SGPA.Server.Models.CMU.Deposito> GetDepositoById(int id)
        {
            var items = Context.Depositos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Cobro);
 
            OnGetDepositoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepositoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepositoCreated(SGPA.Server.Models.CMU.Deposito item);
        partial void OnAfterDepositoCreated(SGPA.Server.Models.CMU.Deposito item);

        public async Task<SGPA.Server.Models.CMU.Deposito> CreateDeposito(SGPA.Server.Models.CMU.Deposito deposito)
        {
            OnDepositoCreated(deposito);

            var existingItem = Context.Depositos
                              .Where(i => i.Id == deposito.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Depositos.Add(deposito);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(deposito).State = EntityState.Detached;
                throw;
            }

            OnAfterDepositoCreated(deposito);

            return deposito;
        }

        public async Task<SGPA.Server.Models.CMU.Deposito> CancelDepositoChanges(SGPA.Server.Models.CMU.Deposito item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepositoUpdated(SGPA.Server.Models.CMU.Deposito item);
        partial void OnAfterDepositoUpdated(SGPA.Server.Models.CMU.Deposito item);

        public async Task<SGPA.Server.Models.CMU.Deposito> UpdateDeposito(int id, SGPA.Server.Models.CMU.Deposito deposito)
        {
            OnDepositoUpdated(deposito);

            var itemToUpdate = Context.Depositos
                              .Where(i => i.Id == deposito.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(deposito);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepositoUpdated(deposito);

            return deposito;
        }

        partial void OnDepositoDeleted(SGPA.Server.Models.CMU.Deposito item);
        partial void OnAfterDepositoDeleted(SGPA.Server.Models.CMU.Deposito item);

        public async Task<SGPA.Server.Models.CMU.Deposito> DeleteDeposito(int id)
        {
            var itemToDelete = Context.Depositos
                              .Where(i => i.Id == id)
                              .Include(i => i.DepositoNominas)
                              .Include(i => i.DepositoNominaNoIdentificada)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepositoDeleted(itemToDelete);


            Context.Depositos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepositoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepositoNominasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepositoNominasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepositoNominasRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNomina> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DepositoNomina>> GetDepositoNominas(Query query = null)
        {
            var items = Context.DepositoNominas.AsQueryable();

            items = items.Include(i => i.Deposito1);
            items = items.Include(i => i.CobroNomina);

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

            OnDepositoNominasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepositoNominaGet(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnGetDepositoNominaById(ref IQueryable<SGPA.Server.Models.CMU.DepositoNomina> items);


        public async Task<SGPA.Server.Models.CMU.DepositoNomina> GetDepositoNominaById(int id)
        {
            var items = Context.DepositoNominas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Deposito1);
            items = items.Include(i => i.CobroNomina);
 
            OnGetDepositoNominaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepositoNominaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepositoNominaCreated(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnAfterDepositoNominaCreated(SGPA.Server.Models.CMU.DepositoNomina item);

        public async Task<SGPA.Server.Models.CMU.DepositoNomina> CreateDepositoNomina(SGPA.Server.Models.CMU.DepositoNomina depositonomina)
        {
            OnDepositoNominaCreated(depositonomina);

            var existingItem = Context.DepositoNominas
                              .Where(i => i.Id == depositonomina.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DepositoNominas.Add(depositonomina);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(depositonomina).State = EntityState.Detached;
                throw;
            }

            OnAfterDepositoNominaCreated(depositonomina);

            return depositonomina;
        }

        public async Task<SGPA.Server.Models.CMU.DepositoNomina> CancelDepositoNominaChanges(SGPA.Server.Models.CMU.DepositoNomina item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepositoNominaUpdated(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnAfterDepositoNominaUpdated(SGPA.Server.Models.CMU.DepositoNomina item);

        public async Task<SGPA.Server.Models.CMU.DepositoNomina> UpdateDepositoNomina(int id, SGPA.Server.Models.CMU.DepositoNomina depositonomina)
        {
            OnDepositoNominaUpdated(depositonomina);

            var itemToUpdate = Context.DepositoNominas
                              .Where(i => i.Id == depositonomina.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(depositonomina);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepositoNominaUpdated(depositonomina);

            return depositonomina;
        }

        partial void OnDepositoNominaDeleted(SGPA.Server.Models.CMU.DepositoNomina item);
        partial void OnAfterDepositoNominaDeleted(SGPA.Server.Models.CMU.DepositoNomina item);

        public async Task<SGPA.Server.Models.CMU.DepositoNomina> DeleteDepositoNomina(int id)
        {
            var itemToDelete = Context.DepositoNominas
                              .Where(i => i.Id == id)
                              .Include(i => i.DepositoNominaMultiBrous)
                              .Include(i => i.DepositoNominaRedPagos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepositoNominaDeleted(itemToDelete);


            Context.DepositoNominas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepositoNominaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepositoNominaMultiBrousToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominamultibrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominamultibrous/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepositoNominaMultiBrousToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominamultibrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominamultibrous/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepositoNominaMultiBrousRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DepositoNominaMultiBrou>> GetDepositoNominaMultiBrous(Query query = null)
        {
            var items = Context.DepositoNominaMultiBrous.AsQueryable();

            items = items.Include(i => i.DepositoNomina);

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

            OnDepositoNominaMultiBrousRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepositoNominaMultiBrouGet(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnGetDepositoNominaMultiBrouById(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> items);


        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> GetDepositoNominaMultiBrouById(int id)
        {
            var items = Context.DepositoNominaMultiBrous
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.DepositoNomina);
 
            OnGetDepositoNominaMultiBrouById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepositoNominaMultiBrouGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepositoNominaMultiBrouCreated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnAfterDepositoNominaMultiBrouCreated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> CreateDepositoNominaMultiBrou(SGPA.Server.Models.CMU.DepositoNominaMultiBrou depositonominamultibrou)
        {
            OnDepositoNominaMultiBrouCreated(depositonominamultibrou);

            var existingItem = Context.DepositoNominaMultiBrous
                              .Where(i => i.Id == depositonominamultibrou.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DepositoNominaMultiBrous.Add(depositonominamultibrou);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(depositonominamultibrou).State = EntityState.Detached;
                throw;
            }

            OnAfterDepositoNominaMultiBrouCreated(depositonominamultibrou);

            return depositonominamultibrou;
        }

        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> CancelDepositoNominaMultiBrouChanges(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepositoNominaMultiBrouUpdated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnAfterDepositoNominaMultiBrouUpdated(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> UpdateDepositoNominaMultiBrou(int id, SGPA.Server.Models.CMU.DepositoNominaMultiBrou depositonominamultibrou)
        {
            OnDepositoNominaMultiBrouUpdated(depositonominamultibrou);

            var itemToUpdate = Context.DepositoNominaMultiBrous
                              .Where(i => i.Id == depositonominamultibrou.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(depositonominamultibrou);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepositoNominaMultiBrouUpdated(depositonominamultibrou);

            return depositonominamultibrou;
        }

        partial void OnDepositoNominaMultiBrouDeleted(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);
        partial void OnAfterDepositoNominaMultiBrouDeleted(SGPA.Server.Models.CMU.DepositoNominaMultiBrou item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaMultiBrou> DeleteDepositoNominaMultiBrou(int id)
        {
            var itemToDelete = Context.DepositoNominaMultiBrous
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepositoNominaMultiBrouDeleted(itemToDelete);


            Context.DepositoNominaMultiBrous.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepositoNominaMultiBrouDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepositoNominaNoIdentificadaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominanoidentificada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominanoidentificada/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepositoNominaNoIdentificadaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominanoidentificada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominanoidentificada/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepositoNominaNoIdentificadaRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum>> GetDepositoNominaNoIdentificada(Query query = null)
        {
            var items = Context.DepositoNominaNoIdentificada.AsQueryable();

            items = items.Include(i => i.Deposito1);

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

            OnDepositoNominaNoIdentificadaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepositoNominaNoIdentificadumGet(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnGetDepositoNominaNoIdentificadumById(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> items);


        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> GetDepositoNominaNoIdentificadumById(int id)
        {
            var items = Context.DepositoNominaNoIdentificada
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Deposito1);
 
            OnGetDepositoNominaNoIdentificadumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepositoNominaNoIdentificadumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepositoNominaNoIdentificadumCreated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnAfterDepositoNominaNoIdentificadumCreated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> CreateDepositoNominaNoIdentificadum(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum depositonominanoidentificadum)
        {
            OnDepositoNominaNoIdentificadumCreated(depositonominanoidentificadum);

            var existingItem = Context.DepositoNominaNoIdentificada
                              .Where(i => i.Id == depositonominanoidentificadum.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DepositoNominaNoIdentificada.Add(depositonominanoidentificadum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(depositonominanoidentificadum).State = EntityState.Detached;
                throw;
            }

            OnAfterDepositoNominaNoIdentificadumCreated(depositonominanoidentificadum);

            return depositonominanoidentificadum;
        }

        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> CancelDepositoNominaNoIdentificadumChanges(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepositoNominaNoIdentificadumUpdated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnAfterDepositoNominaNoIdentificadumUpdated(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> UpdateDepositoNominaNoIdentificadum(int id, SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum depositonominanoidentificadum)
        {
            OnDepositoNominaNoIdentificadumUpdated(depositonominanoidentificadum);

            var itemToUpdate = Context.DepositoNominaNoIdentificada
                              .Where(i => i.Id == depositonominanoidentificadum.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(depositonominanoidentificadum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepositoNominaNoIdentificadumUpdated(depositonominanoidentificadum);

            return depositonominanoidentificadum;
        }

        partial void OnDepositoNominaNoIdentificadumDeleted(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);
        partial void OnAfterDepositoNominaNoIdentificadumDeleted(SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaNoIdentificadum> DeleteDepositoNominaNoIdentificadum(int id)
        {
            var itemToDelete = Context.DepositoNominaNoIdentificada
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepositoNominaNoIdentificadumDeleted(itemToDelete);


            Context.DepositoNominaNoIdentificada.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepositoNominaNoIdentificadumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDepositoNominaRedPagosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominaredpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominaredpagos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDepositoNominaRedPagosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/depositonominaredpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/depositonominaredpagos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDepositoNominaRedPagosRead(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaRedPago> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DepositoNominaRedPago>> GetDepositoNominaRedPagos(Query query = null)
        {
            var items = Context.DepositoNominaRedPagos.AsQueryable();

            items = items.Include(i => i.DepositoNomina);

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

            OnDepositoNominaRedPagosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDepositoNominaRedPagoGet(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnGetDepositoNominaRedPagoById(ref IQueryable<SGPA.Server.Models.CMU.DepositoNominaRedPago> items);


        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> GetDepositoNominaRedPagoById(int id)
        {
            var items = Context.DepositoNominaRedPagos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.DepositoNomina);
 
            OnGetDepositoNominaRedPagoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDepositoNominaRedPagoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDepositoNominaRedPagoCreated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnAfterDepositoNominaRedPagoCreated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> CreateDepositoNominaRedPago(SGPA.Server.Models.CMU.DepositoNominaRedPago depositonominaredpago)
        {
            OnDepositoNominaRedPagoCreated(depositonominaredpago);

            var existingItem = Context.DepositoNominaRedPagos
                              .Where(i => i.Id == depositonominaredpago.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DepositoNominaRedPagos.Add(depositonominaredpago);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(depositonominaredpago).State = EntityState.Detached;
                throw;
            }

            OnAfterDepositoNominaRedPagoCreated(depositonominaredpago);

            return depositonominaredpago;
        }

        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> CancelDepositoNominaRedPagoChanges(SGPA.Server.Models.CMU.DepositoNominaRedPago item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDepositoNominaRedPagoUpdated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnAfterDepositoNominaRedPagoUpdated(SGPA.Server.Models.CMU.DepositoNominaRedPago item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> UpdateDepositoNominaRedPago(int id, SGPA.Server.Models.CMU.DepositoNominaRedPago depositonominaredpago)
        {
            OnDepositoNominaRedPagoUpdated(depositonominaredpago);

            var itemToUpdate = Context.DepositoNominaRedPagos
                              .Where(i => i.Id == depositonominaredpago.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(depositonominaredpago);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDepositoNominaRedPagoUpdated(depositonominaredpago);

            return depositonominaredpago;
        }

        partial void OnDepositoNominaRedPagoDeleted(SGPA.Server.Models.CMU.DepositoNominaRedPago item);
        partial void OnAfterDepositoNominaRedPagoDeleted(SGPA.Server.Models.CMU.DepositoNominaRedPago item);

        public async Task<SGPA.Server.Models.CMU.DepositoNominaRedPago> DeleteDepositoNominaRedPago(int id)
        {
            var itemToDelete = Context.DepositoNominaRedPagos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDepositoNominaRedPagoDeleted(itemToDelete);


            Context.DepositoNominaRedPagos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDepositoNominaRedPagoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDjInactividadMotivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/djinactividadmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/djinactividadmotivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDjInactividadMotivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/djinactividadmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/djinactividadmotivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDjInactividadMotivosRead(ref IQueryable<SGPA.Server.Models.CMU.DjInactividadMotivo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DjInactividadMotivo>> GetDjInactividadMotivos(Query query = null)
        {
            var items = Context.DjInactividadMotivos.AsQueryable();


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

            OnDjInactividadMotivosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDjInactividadMotivoGet(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnGetDjInactividadMotivoById(ref IQueryable<SGPA.Server.Models.CMU.DjInactividadMotivo> items);


        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> GetDjInactividadMotivoById(int id)
        {
            var items = Context.DjInactividadMotivos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetDjInactividadMotivoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDjInactividadMotivoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDjInactividadMotivoCreated(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnAfterDjInactividadMotivoCreated(SGPA.Server.Models.CMU.DjInactividadMotivo item);

        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> CreateDjInactividadMotivo(SGPA.Server.Models.CMU.DjInactividadMotivo djinactividadmotivo)
        {
            OnDjInactividadMotivoCreated(djinactividadmotivo);

            var existingItem = Context.DjInactividadMotivos
                              .Where(i => i.Id == djinactividadmotivo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DjInactividadMotivos.Add(djinactividadmotivo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(djinactividadmotivo).State = EntityState.Detached;
                throw;
            }

            OnAfterDjInactividadMotivoCreated(djinactividadmotivo);

            return djinactividadmotivo;
        }

        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> CancelDjInactividadMotivoChanges(SGPA.Server.Models.CMU.DjInactividadMotivo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDjInactividadMotivoUpdated(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnAfterDjInactividadMotivoUpdated(SGPA.Server.Models.CMU.DjInactividadMotivo item);

        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> UpdateDjInactividadMotivo(int id, SGPA.Server.Models.CMU.DjInactividadMotivo djinactividadmotivo)
        {
            OnDjInactividadMotivoUpdated(djinactividadmotivo);

            var itemToUpdate = Context.DjInactividadMotivos
                              .Where(i => i.Id == djinactividadmotivo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(djinactividadmotivo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDjInactividadMotivoUpdated(djinactividadmotivo);

            return djinactividadmotivo;
        }

        partial void OnDjInactividadMotivoDeleted(SGPA.Server.Models.CMU.DjInactividadMotivo item);
        partial void OnAfterDjInactividadMotivoDeleted(SGPA.Server.Models.CMU.DjInactividadMotivo item);

        public async Task<SGPA.Server.Models.CMU.DjInactividadMotivo> DeleteDjInactividadMotivo(int id)
        {
            var itemToDelete = Context.DjInactividadMotivos
                              .Where(i => i.Id == id)
                              .Include(i => i.ColegiadoDeclaracionJurada)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDjInactividadMotivoDeleted(itemToDelete);


            Context.DjInactividadMotivos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDjInactividadMotivoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDynamicListViewFiltersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/dynamiclistviewfilters/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/dynamiclistviewfilters/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDynamicListViewFiltersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/dynamiclistviewfilters/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/dynamiclistviewfilters/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDynamicListViewFiltersRead(ref IQueryable<SGPA.Server.Models.CMU.DynamicListViewFilter> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.DynamicListViewFilter>> GetDynamicListViewFilters(Query query = null)
        {
            var items = Context.DynamicListViewFilters.AsQueryable();


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

            OnDynamicListViewFiltersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDynamicListViewFilterGet(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnGetDynamicListViewFilterById(ref IQueryable<SGPA.Server.Models.CMU.DynamicListViewFilter> items);


        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> GetDynamicListViewFilterById(int id)
        {
            var items = Context.DynamicListViewFilters
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetDynamicListViewFilterById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDynamicListViewFilterGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDynamicListViewFilterCreated(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnAfterDynamicListViewFilterCreated(SGPA.Server.Models.CMU.DynamicListViewFilter item);

        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> CreateDynamicListViewFilter(SGPA.Server.Models.CMU.DynamicListViewFilter dynamiclistviewfilter)
        {
            OnDynamicListViewFilterCreated(dynamiclistviewfilter);

            var existingItem = Context.DynamicListViewFilters
                              .Where(i => i.Id == dynamiclistviewfilter.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.DynamicListViewFilters.Add(dynamiclistviewfilter);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(dynamiclistviewfilter).State = EntityState.Detached;
                throw;
            }

            OnAfterDynamicListViewFilterCreated(dynamiclistviewfilter);

            return dynamiclistviewfilter;
        }

        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> CancelDynamicListViewFilterChanges(SGPA.Server.Models.CMU.DynamicListViewFilter item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDynamicListViewFilterUpdated(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnAfterDynamicListViewFilterUpdated(SGPA.Server.Models.CMU.DynamicListViewFilter item);

        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> UpdateDynamicListViewFilter(int id, SGPA.Server.Models.CMU.DynamicListViewFilter dynamiclistviewfilter)
        {
            OnDynamicListViewFilterUpdated(dynamiclistviewfilter);

            var itemToUpdate = Context.DynamicListViewFilters
                              .Where(i => i.Id == dynamiclistviewfilter.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(dynamiclistviewfilter);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDynamicListViewFilterUpdated(dynamiclistviewfilter);

            return dynamiclistviewfilter;
        }

        partial void OnDynamicListViewFilterDeleted(SGPA.Server.Models.CMU.DynamicListViewFilter item);
        partial void OnAfterDynamicListViewFilterDeleted(SGPA.Server.Models.CMU.DynamicListViewFilter item);

        public async Task<SGPA.Server.Models.CMU.DynamicListViewFilter> DeleteDynamicListViewFilter(int id)
        {
            var itemToDelete = Context.DynamicListViewFilters
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDynamicListViewFilterDeleted(itemToDelete);


            Context.DynamicListViewFilters.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDynamicListViewFilterDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEmailEnviosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/emailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/emailenvios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmailEnviosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/emailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/emailenvios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmailEnviosRead(ref IQueryable<SGPA.Server.Models.CMU.EmailEnvio> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.EmailEnvio>> GetEmailEnvios(Query query = null)
        {
            var items = Context.EmailEnvios.AsQueryable();


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

            OnEmailEnviosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmailEnvioGet(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnGetEmailEnvioById(ref IQueryable<SGPA.Server.Models.CMU.EmailEnvio> items);


        public async Task<SGPA.Server.Models.CMU.EmailEnvio> GetEmailEnvioById(int id)
        {
            var items = Context.EmailEnvios
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetEmailEnvioById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEmailEnvioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEmailEnvioCreated(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnAfterEmailEnvioCreated(SGPA.Server.Models.CMU.EmailEnvio item);

        public async Task<SGPA.Server.Models.CMU.EmailEnvio> CreateEmailEnvio(SGPA.Server.Models.CMU.EmailEnvio emailenvio)
        {
            OnEmailEnvioCreated(emailenvio);

            var existingItem = Context.EmailEnvios
                              .Where(i => i.Id == emailenvio.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.EmailEnvios.Add(emailenvio);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(emailenvio).State = EntityState.Detached;
                throw;
            }

            OnAfterEmailEnvioCreated(emailenvio);

            return emailenvio;
        }

        public async Task<SGPA.Server.Models.CMU.EmailEnvio> CancelEmailEnvioChanges(SGPA.Server.Models.CMU.EmailEnvio item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEmailEnvioUpdated(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnAfterEmailEnvioUpdated(SGPA.Server.Models.CMU.EmailEnvio item);

        public async Task<SGPA.Server.Models.CMU.EmailEnvio> UpdateEmailEnvio(int id, SGPA.Server.Models.CMU.EmailEnvio emailenvio)
        {
            OnEmailEnvioUpdated(emailenvio);

            var itemToUpdate = Context.EmailEnvios
                              .Where(i => i.Id == emailenvio.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(emailenvio);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEmailEnvioUpdated(emailenvio);

            return emailenvio;
        }

        partial void OnEmailEnvioDeleted(SGPA.Server.Models.CMU.EmailEnvio item);
        partial void OnAfterEmailEnvioDeleted(SGPA.Server.Models.CMU.EmailEnvio item);

        public async Task<SGPA.Server.Models.CMU.EmailEnvio> DeleteEmailEnvio(int id)
        {
            var itemToDelete = Context.EmailEnvios
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmailEnvioDeleted(itemToDelete);


            Context.EmailEnvios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEmailEnvioDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEspecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/especialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEspecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/especialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEspecialidadsRead(ref IQueryable<SGPA.Server.Models.CMU.Especialidad> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Especialidad>> GetEspecialidads(Query query = null)
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

        partial void OnEspecialidadGet(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnGetEspecialidadById(ref IQueryable<SGPA.Server.Models.CMU.Especialidad> items);


        public async Task<SGPA.Server.Models.CMU.Especialidad> GetEspecialidadById(int id)
        {
            var items = Context.Especialidads
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetEspecialidadById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEspecialidadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEspecialidadCreated(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnAfterEspecialidadCreated(SGPA.Server.Models.CMU.Especialidad item);

        public async Task<SGPA.Server.Models.CMU.Especialidad> CreateEspecialidad(SGPA.Server.Models.CMU.Especialidad especialidad)
        {
            OnEspecialidadCreated(especialidad);

            var existingItem = Context.Especialidads
                              .Where(i => i.Id == especialidad.Id)
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

        public async Task<SGPA.Server.Models.CMU.Especialidad> CancelEspecialidadChanges(SGPA.Server.Models.CMU.Especialidad item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEspecialidadUpdated(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnAfterEspecialidadUpdated(SGPA.Server.Models.CMU.Especialidad item);

        public async Task<SGPA.Server.Models.CMU.Especialidad> UpdateEspecialidad(int id, SGPA.Server.Models.CMU.Especialidad especialidad)
        {
            OnEspecialidadUpdated(especialidad);

            var itemToUpdate = Context.Especialidads
                              .Where(i => i.Id == especialidad.Id)
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

        partial void OnEspecialidadDeleted(SGPA.Server.Models.CMU.Especialidad item);
        partial void OnAfterEspecialidadDeleted(SGPA.Server.Models.CMU.Especialidad item);

        public async Task<SGPA.Server.Models.CMU.Especialidad> DeleteEspecialidad(int id)
        {
            var itemToDelete = Context.Especialidads
                              .Where(i => i.Id == id)
                              .Include(i => i.TramiteInfoadjuntaespecialidads)
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
    
        public async Task ExportFacultadTitulosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/facultadtitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/facultadtitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFacultadTitulosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/facultadtitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/facultadtitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFacultadTitulosRead(ref IQueryable<SGPA.Server.Models.CMU.FacultadTitulo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.FacultadTitulo>> GetFacultadTitulos(Query query = null)
        {
            var items = Context.FacultadTitulos.AsQueryable();


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

            OnFacultadTitulosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFacultadTituloGet(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnGetFacultadTituloById(ref IQueryable<SGPA.Server.Models.CMU.FacultadTitulo> items);


        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> GetFacultadTituloById(int id)
        {
            var items = Context.FacultadTitulos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetFacultadTituloById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFacultadTituloGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFacultadTituloCreated(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnAfterFacultadTituloCreated(SGPA.Server.Models.CMU.FacultadTitulo item);

        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> CreateFacultadTitulo(SGPA.Server.Models.CMU.FacultadTitulo facultadtitulo)
        {
            OnFacultadTituloCreated(facultadtitulo);

            var existingItem = Context.FacultadTitulos
                              .Where(i => i.Id == facultadtitulo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.FacultadTitulos.Add(facultadtitulo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(facultadtitulo).State = EntityState.Detached;
                throw;
            }

            OnAfterFacultadTituloCreated(facultadtitulo);

            return facultadtitulo;
        }

        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> CancelFacultadTituloChanges(SGPA.Server.Models.CMU.FacultadTitulo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFacultadTituloUpdated(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnAfterFacultadTituloUpdated(SGPA.Server.Models.CMU.FacultadTitulo item);

        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> UpdateFacultadTitulo(int id, SGPA.Server.Models.CMU.FacultadTitulo facultadtitulo)
        {
            OnFacultadTituloUpdated(facultadtitulo);

            var itemToUpdate = Context.FacultadTitulos
                              .Where(i => i.Id == facultadtitulo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(facultadtitulo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFacultadTituloUpdated(facultadtitulo);

            return facultadtitulo;
        }

        partial void OnFacultadTituloDeleted(SGPA.Server.Models.CMU.FacultadTitulo item);
        partial void OnAfterFacultadTituloDeleted(SGPA.Server.Models.CMU.FacultadTitulo item);

        public async Task<SGPA.Server.Models.CMU.FacultadTitulo> DeleteFacultadTitulo(int id)
        {
            var itemToDelete = Context.FacultadTitulos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFacultadTituloDeleted(itemToDelete);


            Context.FacultadTitulos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFacultadTituloDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFileDataToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/filedata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/filedata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFileDataToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/filedata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/filedata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFileDataRead(ref IQueryable<SGPA.Server.Models.CMU.FileDatum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.FileDatum>> GetFileData(Query query = null)
        {
            var items = Context.FileData.AsQueryable();


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

            OnFileDataRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFileDatumGet(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnGetFileDatumByOid(ref IQueryable<SGPA.Server.Models.CMU.FileDatum> items);


        public async Task<SGPA.Server.Models.CMU.FileDatum> GetFileDatumByOid(Guid oid)
        {
            var items = Context.FileData
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetFileDatumByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFileDatumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFileDatumCreated(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnAfterFileDatumCreated(SGPA.Server.Models.CMU.FileDatum item);

        public async Task<SGPA.Server.Models.CMU.FileDatum> CreateFileDatum(SGPA.Server.Models.CMU.FileDatum filedatum)
        {
            OnFileDatumCreated(filedatum);

            var existingItem = Context.FileData
                              .Where(i => i.Oid == filedatum.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.FileData.Add(filedatum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(filedatum).State = EntityState.Detached;
                throw;
            }

            OnAfterFileDatumCreated(filedatum);

            return filedatum;
        }

        public async Task<SGPA.Server.Models.CMU.FileDatum> CancelFileDatumChanges(SGPA.Server.Models.CMU.FileDatum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFileDatumUpdated(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnAfterFileDatumUpdated(SGPA.Server.Models.CMU.FileDatum item);

        public async Task<SGPA.Server.Models.CMU.FileDatum> UpdateFileDatum(Guid oid, SGPA.Server.Models.CMU.FileDatum filedatum)
        {
            OnFileDatumUpdated(filedatum);

            var itemToUpdate = Context.FileData
                              .Where(i => i.Oid == filedatum.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(filedatum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFileDatumUpdated(filedatum);

            return filedatum;
        }

        partial void OnFileDatumDeleted(SGPA.Server.Models.CMU.FileDatum item);
        partial void OnAfterFileDatumDeleted(SGPA.Server.Models.CMU.FileDatum item);

        public async Task<SGPA.Server.Models.CMU.FileDatum> DeleteFileDatum(Guid oid)
        {
            var itemToDelete = Context.FileData
                              .Where(i => i.Oid == oid)
                              .Include(i => i.ActaConsejos)
                              .Include(i => i.DebitoAdjuntos)
                              .Include(i => i.DeclaracionJuradaAdjuntos)
                              .Include(i => i.SolicitudBajaFileAttachments)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFileDatumDeleted(itemToDelete);


            Context.FileData.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFileDatumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportGrupoContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupocontactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGrupoContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupocontactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGrupoContactosRead(ref IQueryable<SGPA.Server.Models.CMU.GrupoContacto> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.GrupoContacto>> GetGrupoContactos(Query query = null)
        {
            var items = Context.GrupoContactos.AsQueryable();


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

            OnGrupoContactosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGrupoContactoGet(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnGetGrupoContactoById(ref IQueryable<SGPA.Server.Models.CMU.GrupoContacto> items);


        public async Task<SGPA.Server.Models.CMU.GrupoContacto> GetGrupoContactoById(int id)
        {
            var items = Context.GrupoContactos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetGrupoContactoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGrupoContactoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGrupoContactoCreated(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnAfterGrupoContactoCreated(SGPA.Server.Models.CMU.GrupoContacto item);

        public async Task<SGPA.Server.Models.CMU.GrupoContacto> CreateGrupoContacto(SGPA.Server.Models.CMU.GrupoContacto grupocontacto)
        {
            OnGrupoContactoCreated(grupocontacto);

            var existingItem = Context.GrupoContactos
                              .Where(i => i.Id == grupocontacto.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.GrupoContactos.Add(grupocontacto);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(grupocontacto).State = EntityState.Detached;
                throw;
            }

            OnAfterGrupoContactoCreated(grupocontacto);

            return grupocontacto;
        }

        public async Task<SGPA.Server.Models.CMU.GrupoContacto> CancelGrupoContactoChanges(SGPA.Server.Models.CMU.GrupoContacto item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGrupoContactoUpdated(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnAfterGrupoContactoUpdated(SGPA.Server.Models.CMU.GrupoContacto item);

        public async Task<SGPA.Server.Models.CMU.GrupoContacto> UpdateGrupoContacto(int id, SGPA.Server.Models.CMU.GrupoContacto grupocontacto)
        {
            OnGrupoContactoUpdated(grupocontacto);

            var itemToUpdate = Context.GrupoContactos
                              .Where(i => i.Id == grupocontacto.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(grupocontacto);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGrupoContactoUpdated(grupocontacto);

            return grupocontacto;
        }

        partial void OnGrupoContactoDeleted(SGPA.Server.Models.CMU.GrupoContacto item);
        partial void OnAfterGrupoContactoDeleted(SGPA.Server.Models.CMU.GrupoContacto item);

        public async Task<SGPA.Server.Models.CMU.GrupoContacto> DeleteGrupoContacto(int id)
        {
            var itemToDelete = Context.GrupoContactos
                              .Where(i => i.Id == id)
                              .Include(i => i.Contactos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGrupoContactoDeleted(itemToDelete);


            Context.GrupoContactos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGrupoContactoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportGrupoLugarRetiroCarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupolugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupolugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGrupoLugarRetiroCarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/grupolugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/grupolugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGrupoLugarRetiroCarnesRead(ref IQueryable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne>> GetGrupoLugarRetiroCarnes(Query query = null)
        {
            var items = Context.GrupoLugarRetiroCarnes.AsQueryable();


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

            OnGrupoLugarRetiroCarnesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGrupoLugarRetiroCarneGet(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnGetGrupoLugarRetiroCarneById(ref IQueryable<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> items);


        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> GetGrupoLugarRetiroCarneById(int id)
        {
            var items = Context.GrupoLugarRetiroCarnes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetGrupoLugarRetiroCarneById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGrupoLugarRetiroCarneGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGrupoLugarRetiroCarneCreated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnAfterGrupoLugarRetiroCarneCreated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);

        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> CreateGrupoLugarRetiroCarne(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne grupolugarretirocarne)
        {
            OnGrupoLugarRetiroCarneCreated(grupolugarretirocarne);

            var existingItem = Context.GrupoLugarRetiroCarnes
                              .Where(i => i.Id == grupolugarretirocarne.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.GrupoLugarRetiroCarnes.Add(grupolugarretirocarne);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(grupolugarretirocarne).State = EntityState.Detached;
                throw;
            }

            OnAfterGrupoLugarRetiroCarneCreated(grupolugarretirocarne);

            return grupolugarretirocarne;
        }

        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> CancelGrupoLugarRetiroCarneChanges(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGrupoLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnAfterGrupoLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);

        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> UpdateGrupoLugarRetiroCarne(int id, SGPA.Server.Models.CMU.GrupoLugarRetiroCarne grupolugarretirocarne)
        {
            OnGrupoLugarRetiroCarneUpdated(grupolugarretirocarne);

            var itemToUpdate = Context.GrupoLugarRetiroCarnes
                              .Where(i => i.Id == grupolugarretirocarne.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(grupolugarretirocarne);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGrupoLugarRetiroCarneUpdated(grupolugarretirocarne);

            return grupolugarretirocarne;
        }

        partial void OnGrupoLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);
        partial void OnAfterGrupoLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.GrupoLugarRetiroCarne item);

        public async Task<SGPA.Server.Models.CMU.GrupoLugarRetiroCarne> DeleteGrupoLugarRetiroCarne(int id)
        {
            var itemToDelete = Context.GrupoLugarRetiroCarnes
                              .Where(i => i.Id == id)
                              .Include(i => i.LugarRetiroCarnes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGrupoLugarRetiroCarneDeleted(itemToDelete);


            Context.GrupoLugarRetiroCarnes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGrupoLugarRetiroCarneDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportKpiDefinitionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpidefinitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpidefinitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportKpiDefinitionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpidefinitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpidefinitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnKpiDefinitionsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiDefinition> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.KpiDefinition>> GetKpiDefinitions(Query query = null)
        {
            var items = Context.KpiDefinitions.AsQueryable();

            items = items.Include(i => i.KpiInstance1);

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

            OnKpiDefinitionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnKpiDefinitionGet(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnGetKpiDefinitionByOid(ref IQueryable<SGPA.Server.Models.CMU.KpiDefinition> items);


        public async Task<SGPA.Server.Models.CMU.KpiDefinition> GetKpiDefinitionByOid(Guid oid)
        {
            var items = Context.KpiDefinitions
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.KpiInstance1);
 
            OnGetKpiDefinitionByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnKpiDefinitionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnKpiDefinitionCreated(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnAfterKpiDefinitionCreated(SGPA.Server.Models.CMU.KpiDefinition item);

        public async Task<SGPA.Server.Models.CMU.KpiDefinition> CreateKpiDefinition(SGPA.Server.Models.CMU.KpiDefinition kpidefinition)
        {
            OnKpiDefinitionCreated(kpidefinition);

            var existingItem = Context.KpiDefinitions
                              .Where(i => i.Oid == kpidefinition.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.KpiDefinitions.Add(kpidefinition);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(kpidefinition).State = EntityState.Detached;
                throw;
            }

            OnAfterKpiDefinitionCreated(kpidefinition);

            return kpidefinition;
        }

        public async Task<SGPA.Server.Models.CMU.KpiDefinition> CancelKpiDefinitionChanges(SGPA.Server.Models.CMU.KpiDefinition item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnKpiDefinitionUpdated(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnAfterKpiDefinitionUpdated(SGPA.Server.Models.CMU.KpiDefinition item);

        public async Task<SGPA.Server.Models.CMU.KpiDefinition> UpdateKpiDefinition(Guid oid, SGPA.Server.Models.CMU.KpiDefinition kpidefinition)
        {
            OnKpiDefinitionUpdated(kpidefinition);

            var itemToUpdate = Context.KpiDefinitions
                              .Where(i => i.Oid == kpidefinition.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(kpidefinition);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterKpiDefinitionUpdated(kpidefinition);

            return kpidefinition;
        }

        partial void OnKpiDefinitionDeleted(SGPA.Server.Models.CMU.KpiDefinition item);
        partial void OnAfterKpiDefinitionDeleted(SGPA.Server.Models.CMU.KpiDefinition item);

        public async Task<SGPA.Server.Models.CMU.KpiDefinition> DeleteKpiDefinition(Guid oid)
        {
            var itemToDelete = Context.KpiDefinitions
                              .Where(i => i.Oid == oid)
                              .Include(i => i.KpiInstances)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnKpiDefinitionDeleted(itemToDelete);


            Context.KpiDefinitions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterKpiDefinitionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportKpiHistoryItemsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpihistoryitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpihistoryitems/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportKpiHistoryItemsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpihistoryitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpihistoryitems/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnKpiHistoryItemsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiHistoryItem> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.KpiHistoryItem>> GetKpiHistoryItems(Query query = null)
        {
            var items = Context.KpiHistoryItems.AsQueryable();

            items = items.Include(i => i.KpiInstance1);

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

            OnKpiHistoryItemsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnKpiHistoryItemGet(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnGetKpiHistoryItemByOid(ref IQueryable<SGPA.Server.Models.CMU.KpiHistoryItem> items);


        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> GetKpiHistoryItemByOid(Guid oid)
        {
            var items = Context.KpiHistoryItems
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.KpiInstance1);
 
            OnGetKpiHistoryItemByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnKpiHistoryItemGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnKpiHistoryItemCreated(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnAfterKpiHistoryItemCreated(SGPA.Server.Models.CMU.KpiHistoryItem item);

        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> CreateKpiHistoryItem(SGPA.Server.Models.CMU.KpiHistoryItem kpihistoryitem)
        {
            OnKpiHistoryItemCreated(kpihistoryitem);

            var existingItem = Context.KpiHistoryItems
                              .Where(i => i.Oid == kpihistoryitem.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.KpiHistoryItems.Add(kpihistoryitem);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(kpihistoryitem).State = EntityState.Detached;
                throw;
            }

            OnAfterKpiHistoryItemCreated(kpihistoryitem);

            return kpihistoryitem;
        }

        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> CancelKpiHistoryItemChanges(SGPA.Server.Models.CMU.KpiHistoryItem item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnKpiHistoryItemUpdated(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnAfterKpiHistoryItemUpdated(SGPA.Server.Models.CMU.KpiHistoryItem item);

        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> UpdateKpiHistoryItem(Guid oid, SGPA.Server.Models.CMU.KpiHistoryItem kpihistoryitem)
        {
            OnKpiHistoryItemUpdated(kpihistoryitem);

            var itemToUpdate = Context.KpiHistoryItems
                              .Where(i => i.Oid == kpihistoryitem.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(kpihistoryitem);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterKpiHistoryItemUpdated(kpihistoryitem);

            return kpihistoryitem;
        }

        partial void OnKpiHistoryItemDeleted(SGPA.Server.Models.CMU.KpiHistoryItem item);
        partial void OnAfterKpiHistoryItemDeleted(SGPA.Server.Models.CMU.KpiHistoryItem item);

        public async Task<SGPA.Server.Models.CMU.KpiHistoryItem> DeleteKpiHistoryItem(Guid oid)
        {
            var itemToDelete = Context.KpiHistoryItems
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnKpiHistoryItemDeleted(itemToDelete);


            Context.KpiHistoryItems.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterKpiHistoryItemDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportKpiInstancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiinstances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiinstances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportKpiInstancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiinstances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiinstances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnKpiInstancesRead(ref IQueryable<SGPA.Server.Models.CMU.KpiInstance> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.KpiInstance>> GetKpiInstances(Query query = null)
        {
            var items = Context.KpiInstances.AsQueryable();

            items = items.Include(i => i.KpiDefinition1);

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

            OnKpiInstancesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnKpiInstanceGet(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnGetKpiInstanceByOid(ref IQueryable<SGPA.Server.Models.CMU.KpiInstance> items);


        public async Task<SGPA.Server.Models.CMU.KpiInstance> GetKpiInstanceByOid(Guid oid)
        {
            var items = Context.KpiInstances
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.KpiDefinition1);
 
            OnGetKpiInstanceByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnKpiInstanceGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnKpiInstanceCreated(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnAfterKpiInstanceCreated(SGPA.Server.Models.CMU.KpiInstance item);

        public async Task<SGPA.Server.Models.CMU.KpiInstance> CreateKpiInstance(SGPA.Server.Models.CMU.KpiInstance kpiinstance)
        {
            OnKpiInstanceCreated(kpiinstance);

            var existingItem = Context.KpiInstances
                              .Where(i => i.Oid == kpiinstance.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.KpiInstances.Add(kpiinstance);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(kpiinstance).State = EntityState.Detached;
                throw;
            }

            OnAfterKpiInstanceCreated(kpiinstance);

            return kpiinstance;
        }

        public async Task<SGPA.Server.Models.CMU.KpiInstance> CancelKpiInstanceChanges(SGPA.Server.Models.CMU.KpiInstance item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnKpiInstanceUpdated(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnAfterKpiInstanceUpdated(SGPA.Server.Models.CMU.KpiInstance item);

        public async Task<SGPA.Server.Models.CMU.KpiInstance> UpdateKpiInstance(Guid oid, SGPA.Server.Models.CMU.KpiInstance kpiinstance)
        {
            OnKpiInstanceUpdated(kpiinstance);

            var itemToUpdate = Context.KpiInstances
                              .Where(i => i.Oid == kpiinstance.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(kpiinstance);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterKpiInstanceUpdated(kpiinstance);

            return kpiinstance;
        }

        partial void OnKpiInstanceDeleted(SGPA.Server.Models.CMU.KpiInstance item);
        partial void OnAfterKpiInstanceDeleted(SGPA.Server.Models.CMU.KpiInstance item);

        public async Task<SGPA.Server.Models.CMU.KpiInstance> DeleteKpiInstance(Guid oid)
        {
            var itemToDelete = Context.KpiInstances
                              .Where(i => i.Oid == oid)
                              .Include(i => i.KpiDefinitions)
                              .Include(i => i.KpiHistoryItems)
                              .Include(i => i.KpiscorecardscorecardsKpiinstanceindicators)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnKpiInstanceDeleted(itemToDelete);


            Context.KpiInstances.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterKpiInstanceDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportKpiScorecardsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecards/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecards/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportKpiScorecardsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecards/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecards/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnKpiScorecardsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiScorecard> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.KpiScorecard>> GetKpiScorecards(Query query = null)
        {
            var items = Context.KpiScorecards.AsQueryable();


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

            OnKpiScorecardsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnKpiScorecardGet(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnGetKpiScorecardByOid(ref IQueryable<SGPA.Server.Models.CMU.KpiScorecard> items);


        public async Task<SGPA.Server.Models.CMU.KpiScorecard> GetKpiScorecardByOid(Guid oid)
        {
            var items = Context.KpiScorecards
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetKpiScorecardByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnKpiScorecardGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnKpiScorecardCreated(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnAfterKpiScorecardCreated(SGPA.Server.Models.CMU.KpiScorecard item);

        public async Task<SGPA.Server.Models.CMU.KpiScorecard> CreateKpiScorecard(SGPA.Server.Models.CMU.KpiScorecard kpiscorecard)
        {
            OnKpiScorecardCreated(kpiscorecard);

            var existingItem = Context.KpiScorecards
                              .Where(i => i.Oid == kpiscorecard.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.KpiScorecards.Add(kpiscorecard);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(kpiscorecard).State = EntityState.Detached;
                throw;
            }

            OnAfterKpiScorecardCreated(kpiscorecard);

            return kpiscorecard;
        }

        public async Task<SGPA.Server.Models.CMU.KpiScorecard> CancelKpiScorecardChanges(SGPA.Server.Models.CMU.KpiScorecard item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnKpiScorecardUpdated(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnAfterKpiScorecardUpdated(SGPA.Server.Models.CMU.KpiScorecard item);

        public async Task<SGPA.Server.Models.CMU.KpiScorecard> UpdateKpiScorecard(Guid oid, SGPA.Server.Models.CMU.KpiScorecard kpiscorecard)
        {
            OnKpiScorecardUpdated(kpiscorecard);

            var itemToUpdate = Context.KpiScorecards
                              .Where(i => i.Oid == kpiscorecard.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(kpiscorecard);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterKpiScorecardUpdated(kpiscorecard);

            return kpiscorecard;
        }

        partial void OnKpiScorecardDeleted(SGPA.Server.Models.CMU.KpiScorecard item);
        partial void OnAfterKpiScorecardDeleted(SGPA.Server.Models.CMU.KpiScorecard item);

        public async Task<SGPA.Server.Models.CMU.KpiScorecard> DeleteKpiScorecard(Guid oid)
        {
            var itemToDelete = Context.KpiScorecards
                              .Where(i => i.Oid == oid)
                              .Include(i => i.KpiscorecardscorecardsKpiinstanceindicators)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnKpiScorecardDeleted(itemToDelete);


            Context.KpiScorecards.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterKpiScorecardDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportKpiscorecardscorecardsKpiinstanceindicatorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecardscorecardskpiinstanceindicators/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecardscorecardskpiinstanceindicators/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportKpiscorecardscorecardsKpiinstanceindicatorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/kpiscorecardscorecardskpiinstanceindicators/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/kpiscorecardscorecardskpiinstanceindicators/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorsRead(ref IQueryable<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator>> GetKpiscorecardscorecardsKpiinstanceindicators(Query query = null)
        {
            var items = Context.KpiscorecardscorecardsKpiinstanceindicators.AsQueryable();

            items = items.Include(i => i.KpiInstance);
            items = items.Include(i => i.KpiScorecard);

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

            OnKpiscorecardscorecardsKpiinstanceindicatorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorGet(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnGetKpiscorecardscorecardsKpiinstanceindicatorByOid(ref IQueryable<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> items);


        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> GetKpiscorecardscorecardsKpiinstanceindicatorByOid(Guid oid)
        {
            var items = Context.KpiscorecardscorecardsKpiinstanceindicators
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.KpiInstance);
            items = items.Include(i => i.KpiScorecard);
 
            OnGetKpiscorecardscorecardsKpiinstanceindicatorByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnKpiscorecardscorecardsKpiinstanceindicatorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorCreated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnAfterKpiscorecardscorecardsKpiinstanceindicatorCreated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);

        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> CreateKpiscorecardscorecardsKpiinstanceindicator(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator kpiscorecardscorecardskpiinstanceindicator)
        {
            OnKpiscorecardscorecardsKpiinstanceindicatorCreated(kpiscorecardscorecardskpiinstanceindicator);

            var existingItem = Context.KpiscorecardscorecardsKpiinstanceindicators
                              .Where(i => i.OID == kpiscorecardscorecardskpiinstanceindicator.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.KpiscorecardscorecardsKpiinstanceindicators.Add(kpiscorecardscorecardskpiinstanceindicator);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(kpiscorecardscorecardskpiinstanceindicator).State = EntityState.Detached;
                throw;
            }

            OnAfterKpiscorecardscorecardsKpiinstanceindicatorCreated(kpiscorecardscorecardskpiinstanceindicator);

            return kpiscorecardscorecardskpiinstanceindicator;
        }

        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> CancelKpiscorecardscorecardsKpiinstanceindicatorChanges(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorUpdated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnAfterKpiscorecardscorecardsKpiinstanceindicatorUpdated(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);

        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> UpdateKpiscorecardscorecardsKpiinstanceindicator(Guid oid, SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator kpiscorecardscorecardskpiinstanceindicator)
        {
            OnKpiscorecardscorecardsKpiinstanceindicatorUpdated(kpiscorecardscorecardskpiinstanceindicator);

            var itemToUpdate = Context.KpiscorecardscorecardsKpiinstanceindicators
                              .Where(i => i.OID == kpiscorecardscorecardskpiinstanceindicator.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(kpiscorecardscorecardskpiinstanceindicator);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterKpiscorecardscorecardsKpiinstanceindicatorUpdated(kpiscorecardscorecardskpiinstanceindicator);

            return kpiscorecardscorecardskpiinstanceindicator;
        }

        partial void OnKpiscorecardscorecardsKpiinstanceindicatorDeleted(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);
        partial void OnAfterKpiscorecardscorecardsKpiinstanceindicatorDeleted(SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator item);

        public async Task<SGPA.Server.Models.CMU.KpiscorecardscorecardsKpiinstanceindicator> DeleteKpiscorecardscorecardsKpiinstanceindicator(Guid oid)
        {
            var itemToDelete = Context.KpiscorecardscorecardsKpiinstanceindicators
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnKpiscorecardscorecardsKpiinstanceindicatorDeleted(itemToDelete);


            Context.KpiscorecardscorecardsKpiinstanceindicators.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterKpiscorecardscorecardsKpiinstanceindicatorDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportLugarRetiroCarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/lugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/lugarretirocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLugarRetiroCarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/lugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/lugarretirocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLugarRetiroCarnesRead(ref IQueryable<SGPA.Server.Models.CMU.LugarRetiroCarne> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.LugarRetiroCarne>> GetLugarRetiroCarnes(Query query = null)
        {
            var items = Context.LugarRetiroCarnes.AsQueryable();

            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.GrupoLugarRetiroCarne);

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

            OnLugarRetiroCarnesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLugarRetiroCarneGet(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnGetLugarRetiroCarneById(ref IQueryable<SGPA.Server.Models.CMU.LugarRetiroCarne> items);


        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> GetLugarRetiroCarneById(int id)
        {
            var items = Context.LugarRetiroCarnes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.GrupoLugarRetiroCarne);
 
            OnGetLugarRetiroCarneById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnLugarRetiroCarneGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnLugarRetiroCarneCreated(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnAfterLugarRetiroCarneCreated(SGPA.Server.Models.CMU.LugarRetiroCarne item);

        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> CreateLugarRetiroCarne(SGPA.Server.Models.CMU.LugarRetiroCarne lugarretirocarne)
        {
            OnLugarRetiroCarneCreated(lugarretirocarne);

            var existingItem = Context.LugarRetiroCarnes
                              .Where(i => i.Id == lugarretirocarne.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.LugarRetiroCarnes.Add(lugarretirocarne);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(lugarretirocarne).State = EntityState.Detached;
                throw;
            }

            OnAfterLugarRetiroCarneCreated(lugarretirocarne);

            return lugarretirocarne;
        }

        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> CancelLugarRetiroCarneChanges(SGPA.Server.Models.CMU.LugarRetiroCarne item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnAfterLugarRetiroCarneUpdated(SGPA.Server.Models.CMU.LugarRetiroCarne item);

        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> UpdateLugarRetiroCarne(int id, SGPA.Server.Models.CMU.LugarRetiroCarne lugarretirocarne)
        {
            OnLugarRetiroCarneUpdated(lugarretirocarne);

            var itemToUpdate = Context.LugarRetiroCarnes
                              .Where(i => i.Id == lugarretirocarne.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(lugarretirocarne);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterLugarRetiroCarneUpdated(lugarretirocarne);

            return lugarretirocarne;
        }

        partial void OnLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.LugarRetiroCarne item);
        partial void OnAfterLugarRetiroCarneDeleted(SGPA.Server.Models.CMU.LugarRetiroCarne item);

        public async Task<SGPA.Server.Models.CMU.LugarRetiroCarne> DeleteLugarRetiroCarne(int id)
        {
            var itemToDelete = Context.LugarRetiroCarnes
                              .Where(i => i.Id == id)
                              .Include(i => i.TramiteCarnes)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLugarRetiroCarneDeleted(itemToDelete);


            Context.LugarRetiroCarnes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLugarRetiroCarneDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMensajePushesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMensajePushesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMensajePushesRead(ref IQueryable<SGPA.Server.Models.CMU.MensajePush> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MensajePush>> GetMensajePushes(Query query = null)
        {
            var items = Context.MensajePushes.AsQueryable();


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

            OnMensajePushesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMensajePushGet(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnGetMensajePushByOid(ref IQueryable<SGPA.Server.Models.CMU.MensajePush> items);


        public async Task<SGPA.Server.Models.CMU.MensajePush> GetMensajePushByOid(int oid)
        {
            var items = Context.MensajePushes
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

 
            OnGetMensajePushByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMensajePushGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMensajePushCreated(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnAfterMensajePushCreated(SGPA.Server.Models.CMU.MensajePush item);

        public async Task<SGPA.Server.Models.CMU.MensajePush> CreateMensajePush(SGPA.Server.Models.CMU.MensajePush mensajepush)
        {
            OnMensajePushCreated(mensajepush);

            var existingItem = Context.MensajePushes
                              .Where(i => i.OID == mensajepush.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MensajePushes.Add(mensajepush);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(mensajepush).State = EntityState.Detached;
                throw;
            }

            OnAfterMensajePushCreated(mensajepush);

            return mensajepush;
        }

        public async Task<SGPA.Server.Models.CMU.MensajePush> CancelMensajePushChanges(SGPA.Server.Models.CMU.MensajePush item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMensajePushUpdated(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnAfterMensajePushUpdated(SGPA.Server.Models.CMU.MensajePush item);

        public async Task<SGPA.Server.Models.CMU.MensajePush> UpdateMensajePush(int oid, SGPA.Server.Models.CMU.MensajePush mensajepush)
        {
            OnMensajePushUpdated(mensajepush);

            var itemToUpdate = Context.MensajePushes
                              .Where(i => i.OID == mensajepush.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(mensajepush);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMensajePushUpdated(mensajepush);

            return mensajepush;
        }

        partial void OnMensajePushDeleted(SGPA.Server.Models.CMU.MensajePush item);
        partial void OnAfterMensajePushDeleted(SGPA.Server.Models.CMU.MensajePush item);

        public async Task<SGPA.Server.Models.CMU.MensajePush> DeleteMensajePush(int oid)
        {
            var itemToDelete = Context.MensajePushes
                              .Where(i => i.OID == oid)
                              .Include(i => i.MensajePushAdds)
                              .Include(i => i.MensajeSegmentos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMensajePushDeleted(itemToDelete);


            Context.MensajePushes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMensajePushDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMensajePushAddsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushadds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushadds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMensajePushAddsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajepushadds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajepushadds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMensajePushAddsRead(ref IQueryable<SGPA.Server.Models.CMU.MensajePushAdd> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MensajePushAdd>> GetMensajePushAdds(Query query = null)
        {
            var items = Context.MensajePushAdds.AsQueryable();

            items = items.Include(i => i.MensajePush);

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

            OnMensajePushAddsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMensajePushAddGet(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnGetMensajePushAddByOid(ref IQueryable<SGPA.Server.Models.CMU.MensajePushAdd> items);


        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> GetMensajePushAddByOid(int oid)
        {
            var items = Context.MensajePushAdds
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.MensajePush);
 
            OnGetMensajePushAddByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMensajePushAddGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMensajePushAddCreated(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnAfterMensajePushAddCreated(SGPA.Server.Models.CMU.MensajePushAdd item);

        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> CreateMensajePushAdd(SGPA.Server.Models.CMU.MensajePushAdd mensajepushadd)
        {
            OnMensajePushAddCreated(mensajepushadd);

            var existingItem = Context.MensajePushAdds
                              .Where(i => i.OID == mensajepushadd.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MensajePushAdds.Add(mensajepushadd);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(mensajepushadd).State = EntityState.Detached;
                throw;
            }

            OnAfterMensajePushAddCreated(mensajepushadd);

            return mensajepushadd;
        }

        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> CancelMensajePushAddChanges(SGPA.Server.Models.CMU.MensajePushAdd item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMensajePushAddUpdated(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnAfterMensajePushAddUpdated(SGPA.Server.Models.CMU.MensajePushAdd item);

        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> UpdateMensajePushAdd(int oid, SGPA.Server.Models.CMU.MensajePushAdd mensajepushadd)
        {
            OnMensajePushAddUpdated(mensajepushadd);

            var itemToUpdate = Context.MensajePushAdds
                              .Where(i => i.OID == mensajepushadd.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(mensajepushadd);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMensajePushAddUpdated(mensajepushadd);

            return mensajepushadd;
        }

        partial void OnMensajePushAddDeleted(SGPA.Server.Models.CMU.MensajePushAdd item);
        partial void OnAfterMensajePushAddDeleted(SGPA.Server.Models.CMU.MensajePushAdd item);

        public async Task<SGPA.Server.Models.CMU.MensajePushAdd> DeleteMensajePushAdd(int oid)
        {
            var itemToDelete = Context.MensajePushAdds
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMensajePushAddDeleted(itemToDelete);


            Context.MensajePushAdds.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMensajePushAddDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMensajeSegmentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajesegmentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajesegmentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMensajeSegmentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/mensajesegmentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/mensajesegmentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMensajeSegmentosRead(ref IQueryable<SGPA.Server.Models.CMU.MensajeSegmento> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MensajeSegmento>> GetMensajeSegmentos(Query query = null)
        {
            var items = Context.MensajeSegmentos.AsQueryable();

            items = items.Include(i => i.MensajePush);

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

            OnMensajeSegmentosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMensajeSegmentoGet(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnGetMensajeSegmentoByOid(ref IQueryable<SGPA.Server.Models.CMU.MensajeSegmento> items);


        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> GetMensajeSegmentoByOid(int oid)
        {
            var items = Context.MensajeSegmentos
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.MensajePush);
 
            OnGetMensajeSegmentoByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMensajeSegmentoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMensajeSegmentoCreated(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnAfterMensajeSegmentoCreated(SGPA.Server.Models.CMU.MensajeSegmento item);

        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> CreateMensajeSegmento(SGPA.Server.Models.CMU.MensajeSegmento mensajesegmento)
        {
            OnMensajeSegmentoCreated(mensajesegmento);

            var existingItem = Context.MensajeSegmentos
                              .Where(i => i.OID == mensajesegmento.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MensajeSegmentos.Add(mensajesegmento);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(mensajesegmento).State = EntityState.Detached;
                throw;
            }

            OnAfterMensajeSegmentoCreated(mensajesegmento);

            return mensajesegmento;
        }

        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> CancelMensajeSegmentoChanges(SGPA.Server.Models.CMU.MensajeSegmento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMensajeSegmentoUpdated(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnAfterMensajeSegmentoUpdated(SGPA.Server.Models.CMU.MensajeSegmento item);

        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> UpdateMensajeSegmento(int oid, SGPA.Server.Models.CMU.MensajeSegmento mensajesegmento)
        {
            OnMensajeSegmentoUpdated(mensajesegmento);

            var itemToUpdate = Context.MensajeSegmentos
                              .Where(i => i.OID == mensajesegmento.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(mensajesegmento);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMensajeSegmentoUpdated(mensajesegmento);

            return mensajesegmento;
        }

        partial void OnMensajeSegmentoDeleted(SGPA.Server.Models.CMU.MensajeSegmento item);
        partial void OnAfterMensajeSegmentoDeleted(SGPA.Server.Models.CMU.MensajeSegmento item);

        public async Task<SGPA.Server.Models.CMU.MensajeSegmento> DeleteMensajeSegmento(int oid)
        {
            var itemToDelete = Context.MensajeSegmentos
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMensajeSegmentoDeleted(itemToDelete);


            Context.MensajeSegmentos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMensajeSegmentoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportModuleInfosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/moduleinfos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/moduleinfos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportModuleInfosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/moduleinfos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/moduleinfos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnModuleInfosRead(ref IQueryable<SGPA.Server.Models.CMU.ModuleInfo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ModuleInfo>> GetModuleInfos(Query query = null)
        {
            var items = Context.ModuleInfos.AsQueryable();


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

            OnModuleInfosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnModuleInfoGet(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnGetModuleInfoById(ref IQueryable<SGPA.Server.Models.CMU.ModuleInfo> items);


        public async Task<SGPA.Server.Models.CMU.ModuleInfo> GetModuleInfoById(int id)
        {
            var items = Context.ModuleInfos
                              .AsNoTracking()
                              .Where(i => i.ID == id);

 
            OnGetModuleInfoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnModuleInfoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnModuleInfoCreated(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnAfterModuleInfoCreated(SGPA.Server.Models.CMU.ModuleInfo item);

        public async Task<SGPA.Server.Models.CMU.ModuleInfo> CreateModuleInfo(SGPA.Server.Models.CMU.ModuleInfo moduleinfo)
        {
            OnModuleInfoCreated(moduleinfo);

            var existingItem = Context.ModuleInfos
                              .Where(i => i.ID == moduleinfo.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ModuleInfos.Add(moduleinfo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(moduleinfo).State = EntityState.Detached;
                throw;
            }

            OnAfterModuleInfoCreated(moduleinfo);

            return moduleinfo;
        }

        public async Task<SGPA.Server.Models.CMU.ModuleInfo> CancelModuleInfoChanges(SGPA.Server.Models.CMU.ModuleInfo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnModuleInfoUpdated(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnAfterModuleInfoUpdated(SGPA.Server.Models.CMU.ModuleInfo item);

        public async Task<SGPA.Server.Models.CMU.ModuleInfo> UpdateModuleInfo(int id, SGPA.Server.Models.CMU.ModuleInfo moduleinfo)
        {
            OnModuleInfoUpdated(moduleinfo);

            var itemToUpdate = Context.ModuleInfos
                              .Where(i => i.ID == moduleinfo.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(moduleinfo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterModuleInfoUpdated(moduleinfo);

            return moduleinfo;
        }

        partial void OnModuleInfoDeleted(SGPA.Server.Models.CMU.ModuleInfo item);
        partial void OnAfterModuleInfoDeleted(SGPA.Server.Models.CMU.ModuleInfo item);

        public async Task<SGPA.Server.Models.CMU.ModuleInfo> DeleteModuleInfo(int id)
        {
            var itemToDelete = Context.ModuleInfos
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnModuleInfoDeleted(itemToDelete);


            Context.ModuleInfos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterModuleInfoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMovimientoCuentaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuenta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMovimientoCuentaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuenta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMovimientoCuentaRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MovimientoCuentum>> GetMovimientoCuenta(Query query = null)
        {
            var items = Context.MovimientoCuenta.AsQueryable();

            items = items.Include(i => i.AjusteRetroactivo);
            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.MovimientoCuentum1);
            items = items.Include(i => i.MovimientoTipo1);
            items = items.Include(i => i.OrigenMovimiento);

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

            OnMovimientoCuentaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMovimientoCuentumGet(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnGetMovimientoCuentumById(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentum> items);


        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> GetMovimientoCuentumById(int id)
        {
            var items = Context.MovimientoCuenta
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AjusteRetroactivo);
            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.MovimientoCuentum1);
            items = items.Include(i => i.MovimientoTipo1);
            items = items.Include(i => i.OrigenMovimiento);
 
            OnGetMovimientoCuentumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMovimientoCuentumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMovimientoCuentumCreated(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnAfterMovimientoCuentumCreated(SGPA.Server.Models.CMU.MovimientoCuentum item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> CreateMovimientoCuentum(SGPA.Server.Models.CMU.MovimientoCuentum movimientocuentum)
        {
            OnMovimientoCuentumCreated(movimientocuentum);

            var existingItem = Context.MovimientoCuenta
                              .Where(i => i.Id == movimientocuentum.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MovimientoCuenta.Add(movimientocuentum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(movimientocuentum).State = EntityState.Detached;
                throw;
            }

            OnAfterMovimientoCuentumCreated(movimientocuentum);

            return movimientocuentum;
        }

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> CancelMovimientoCuentumChanges(SGPA.Server.Models.CMU.MovimientoCuentum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMovimientoCuentumUpdated(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnAfterMovimientoCuentumUpdated(SGPA.Server.Models.CMU.MovimientoCuentum item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> UpdateMovimientoCuentum(int id, SGPA.Server.Models.CMU.MovimientoCuentum movimientocuentum)
        {
            OnMovimientoCuentumUpdated(movimientocuentum);

            var itemToUpdate = Context.MovimientoCuenta
                              .Where(i => i.Id == movimientocuentum.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(movimientocuentum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMovimientoCuentumUpdated(movimientocuentum);

            return movimientocuentum;
        }

        partial void OnMovimientoCuentumDeleted(SGPA.Server.Models.CMU.MovimientoCuentum item);
        partial void OnAfterMovimientoCuentumDeleted(SGPA.Server.Models.CMU.MovimientoCuentum item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentum> DeleteMovimientoCuentum(int id)
        {
            var itemToDelete = Context.MovimientoCuenta
                              .Where(i => i.Id == id)
                              .Include(i => i.CobroNominas)
                              .Include(i => i.MovimientoCuenta1)
                              .Include(i => i.MovimientoCuentaCuota)
                              .Include(i => i.MovimientoCuentaManuals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMovimientoCuentumDeleted(itemToDelete);


            Context.MovimientoCuenta.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMovimientoCuentumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMovimientoCuentaCuotaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentacuota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentacuota/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMovimientoCuentaCuotaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentacuota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentacuota/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMovimientoCuentaCuotaRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaCuotum>> GetMovimientoCuentaCuota(Query query = null)
        {
            var items = Context.MovimientoCuentaCuota.AsQueryable();

            items = items.Include(i => i.CategoriaColegiado);
            items = items.Include(i => i.MovimientoCuentum);

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

            OnMovimientoCuentaCuotaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMovimientoCuentaCuotumGet(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnGetMovimientoCuentaCuotumById(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> items);


        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> GetMovimientoCuentaCuotumById(int id)
        {
            var items = Context.MovimientoCuentaCuota
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CategoriaColegiado);
            items = items.Include(i => i.MovimientoCuentum);
 
            OnGetMovimientoCuentaCuotumById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMovimientoCuentaCuotumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMovimientoCuentaCuotumCreated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnAfterMovimientoCuentaCuotumCreated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> CreateMovimientoCuentaCuotum(SGPA.Server.Models.CMU.MovimientoCuentaCuotum movimientocuentacuotum)
        {
            OnMovimientoCuentaCuotumCreated(movimientocuentacuotum);

            var existingItem = Context.MovimientoCuentaCuota
                              .Where(i => i.Id == movimientocuentacuotum.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MovimientoCuentaCuota.Add(movimientocuentacuotum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(movimientocuentacuotum).State = EntityState.Detached;
                throw;
            }

            OnAfterMovimientoCuentaCuotumCreated(movimientocuentacuotum);

            return movimientocuentacuotum;
        }

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> CancelMovimientoCuentaCuotumChanges(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMovimientoCuentaCuotumUpdated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnAfterMovimientoCuentaCuotumUpdated(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> UpdateMovimientoCuentaCuotum(int id, SGPA.Server.Models.CMU.MovimientoCuentaCuotum movimientocuentacuotum)
        {
            OnMovimientoCuentaCuotumUpdated(movimientocuentacuotum);

            var itemToUpdate = Context.MovimientoCuentaCuota
                              .Where(i => i.Id == movimientocuentacuotum.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(movimientocuentacuotum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMovimientoCuentaCuotumUpdated(movimientocuentacuotum);

            return movimientocuentacuotum;
        }

        partial void OnMovimientoCuentaCuotumDeleted(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);
        partial void OnAfterMovimientoCuentaCuotumDeleted(SGPA.Server.Models.CMU.MovimientoCuentaCuotum item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaCuotum> DeleteMovimientoCuentaCuotum(int id)
        {
            var itemToDelete = Context.MovimientoCuentaCuota
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMovimientoCuentaCuotumDeleted(itemToDelete);


            Context.MovimientoCuentaCuota.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMovimientoCuentaCuotumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMovimientoCuentaManualsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentamanuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentamanuals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMovimientoCuentaManualsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientocuentamanuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientocuentamanuals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMovimientoCuentaManualsRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaManual> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaManual>> GetMovimientoCuentaManuals(Query query = null)
        {
            var items = Context.MovimientoCuentaManuals.AsQueryable();

            items = items.Include(i => i.MovimientoCuentum);

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

            OnMovimientoCuentaManualsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMovimientoCuentaManualGet(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnGetMovimientoCuentaManualById(ref IQueryable<SGPA.Server.Models.CMU.MovimientoCuentaManual> items);


        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> GetMovimientoCuentaManualById(int id)
        {
            var items = Context.MovimientoCuentaManuals
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.MovimientoCuentum);
 
            OnGetMovimientoCuentaManualById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMovimientoCuentaManualGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMovimientoCuentaManualCreated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnAfterMovimientoCuentaManualCreated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> CreateMovimientoCuentaManual(SGPA.Server.Models.CMU.MovimientoCuentaManual movimientocuentamanual)
        {
            OnMovimientoCuentaManualCreated(movimientocuentamanual);

            var existingItem = Context.MovimientoCuentaManuals
                              .Where(i => i.Id == movimientocuentamanual.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MovimientoCuentaManuals.Add(movimientocuentamanual);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(movimientocuentamanual).State = EntityState.Detached;
                throw;
            }

            OnAfterMovimientoCuentaManualCreated(movimientocuentamanual);

            return movimientocuentamanual;
        }

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> CancelMovimientoCuentaManualChanges(SGPA.Server.Models.CMU.MovimientoCuentaManual item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMovimientoCuentaManualUpdated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnAfterMovimientoCuentaManualUpdated(SGPA.Server.Models.CMU.MovimientoCuentaManual item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> UpdateMovimientoCuentaManual(int id, SGPA.Server.Models.CMU.MovimientoCuentaManual movimientocuentamanual)
        {
            OnMovimientoCuentaManualUpdated(movimientocuentamanual);

            var itemToUpdate = Context.MovimientoCuentaManuals
                              .Where(i => i.Id == movimientocuentamanual.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(movimientocuentamanual);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMovimientoCuentaManualUpdated(movimientocuentamanual);

            return movimientocuentamanual;
        }

        partial void OnMovimientoCuentaManualDeleted(SGPA.Server.Models.CMU.MovimientoCuentaManual item);
        partial void OnAfterMovimientoCuentaManualDeleted(SGPA.Server.Models.CMU.MovimientoCuentaManual item);

        public async Task<SGPA.Server.Models.CMU.MovimientoCuentaManual> DeleteMovimientoCuentaManual(int id)
        {
            var itemToDelete = Context.MovimientoCuentaManuals
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMovimientoCuentaManualDeleted(itemToDelete);


            Context.MovimientoCuentaManuals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMovimientoCuentaManualDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMovimientoTiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientotipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientotipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMovimientoTiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/movimientotipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/movimientotipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMovimientoTiposRead(ref IQueryable<SGPA.Server.Models.CMU.MovimientoTipo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MovimientoTipo>> GetMovimientoTipos(Query query = null)
        {
            var items = Context.MovimientoTipos.AsQueryable();


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

            OnMovimientoTiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMovimientoTipoGet(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnGetMovimientoTipoById(ref IQueryable<SGPA.Server.Models.CMU.MovimientoTipo> items);


        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> GetMovimientoTipoById(int id)
        {
            var items = Context.MovimientoTipos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetMovimientoTipoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMovimientoTipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMovimientoTipoCreated(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnAfterMovimientoTipoCreated(SGPA.Server.Models.CMU.MovimientoTipo item);

        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> CreateMovimientoTipo(SGPA.Server.Models.CMU.MovimientoTipo movimientotipo)
        {
            OnMovimientoTipoCreated(movimientotipo);

            var existingItem = Context.MovimientoTipos
                              .Where(i => i.Id == movimientotipo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MovimientoTipos.Add(movimientotipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(movimientotipo).State = EntityState.Detached;
                throw;
            }

            OnAfterMovimientoTipoCreated(movimientotipo);

            return movimientotipo;
        }

        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> CancelMovimientoTipoChanges(SGPA.Server.Models.CMU.MovimientoTipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMovimientoTipoUpdated(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnAfterMovimientoTipoUpdated(SGPA.Server.Models.CMU.MovimientoTipo item);

        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> UpdateMovimientoTipo(int id, SGPA.Server.Models.CMU.MovimientoTipo movimientotipo)
        {
            OnMovimientoTipoUpdated(movimientotipo);

            var itemToUpdate = Context.MovimientoTipos
                              .Where(i => i.Id == movimientotipo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(movimientotipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMovimientoTipoUpdated(movimientotipo);

            return movimientotipo;
        }

        partial void OnMovimientoTipoDeleted(SGPA.Server.Models.CMU.MovimientoTipo item);
        partial void OnAfterMovimientoTipoDeleted(SGPA.Server.Models.CMU.MovimientoTipo item);

        public async Task<SGPA.Server.Models.CMU.MovimientoTipo> DeleteMovimientoTipo(int id)
        {
            var itemToDelete = Context.MovimientoTipos
                              .Where(i => i.Id == id)
                              .Include(i => i.MovimientoCuenta)
                              .Include(i => i.Parametros)
                              .Include(i => i.Parametros1)
                              .Include(i => i.Parametros2)
                              .Include(i => i.Parametros3)
                              .Include(i => i.RolrolesMovimientotipomovimientostipos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMovimientoTipoDeleted(itemToDelete);


            Context.MovimientoTipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMovimientoTipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMyFileDataToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/myfiledata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/myfiledata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMyFileDataToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/myfiledata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/myfiledata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMyFileDataRead(ref IQueryable<SGPA.Server.Models.CMU.MyFileDatum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.MyFileDatum>> GetMyFileData(Query query = null)
        {
            var items = Context.MyFileData.AsQueryable();


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

            OnMyFileDataRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMyFileDatumGet(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnGetMyFileDatumByOid(ref IQueryable<SGPA.Server.Models.CMU.MyFileDatum> items);


        public async Task<SGPA.Server.Models.CMU.MyFileDatum> GetMyFileDatumByOid(Guid oid)
        {
            var items = Context.MyFileData
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetMyFileDatumByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMyFileDatumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMyFileDatumCreated(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnAfterMyFileDatumCreated(SGPA.Server.Models.CMU.MyFileDatum item);

        public async Task<SGPA.Server.Models.CMU.MyFileDatum> CreateMyFileDatum(SGPA.Server.Models.CMU.MyFileDatum myfiledatum)
        {
            OnMyFileDatumCreated(myfiledatum);

            var existingItem = Context.MyFileData
                              .Where(i => i.Oid == myfiledatum.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.MyFileData.Add(myfiledatum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(myfiledatum).State = EntityState.Detached;
                throw;
            }

            OnAfterMyFileDatumCreated(myfiledatum);

            return myfiledatum;
        }

        public async Task<SGPA.Server.Models.CMU.MyFileDatum> CancelMyFileDatumChanges(SGPA.Server.Models.CMU.MyFileDatum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMyFileDatumUpdated(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnAfterMyFileDatumUpdated(SGPA.Server.Models.CMU.MyFileDatum item);

        public async Task<SGPA.Server.Models.CMU.MyFileDatum> UpdateMyFileDatum(Guid oid, SGPA.Server.Models.CMU.MyFileDatum myfiledatum)
        {
            OnMyFileDatumUpdated(myfiledatum);

            var itemToUpdate = Context.MyFileData
                              .Where(i => i.Oid == myfiledatum.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(myfiledatum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMyFileDatumUpdated(myfiledatum);

            return myfiledatum;
        }

        partial void OnMyFileDatumDeleted(SGPA.Server.Models.CMU.MyFileDatum item);
        partial void OnAfterMyFileDatumDeleted(SGPA.Server.Models.CMU.MyFileDatum item);

        public async Task<SGPA.Server.Models.CMU.MyFileDatum> DeleteMyFileDatum(Guid oid)
        {
            var itemToDelete = Context.MyFileData
                              .Where(i => i.Oid == oid)
                              .Include(i => i.SalaReservas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMyFileDatumDeleted(itemToDelete);


            Context.MyFileData.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMyFileDatumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportOrigenMovimientosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/origenmovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/origenmovimientos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrigenMovimientosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/origenmovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/origenmovimientos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrigenMovimientosRead(ref IQueryable<SGPA.Server.Models.CMU.OrigenMovimiento> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.OrigenMovimiento>> GetOrigenMovimientos(Query query = null)
        {
            var items = Context.OrigenMovimientos.AsQueryable();


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

            OnOrigenMovimientosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrigenMovimientoGet(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnGetOrigenMovimientoById(ref IQueryable<SGPA.Server.Models.CMU.OrigenMovimiento> items);


        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> GetOrigenMovimientoById(int id)
        {
            var items = Context.OrigenMovimientos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetOrigenMovimientoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnOrigenMovimientoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnOrigenMovimientoCreated(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnAfterOrigenMovimientoCreated(SGPA.Server.Models.CMU.OrigenMovimiento item);

        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> CreateOrigenMovimiento(SGPA.Server.Models.CMU.OrigenMovimiento origenmovimiento)
        {
            OnOrigenMovimientoCreated(origenmovimiento);

            var existingItem = Context.OrigenMovimientos
                              .Where(i => i.Id == origenmovimiento.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.OrigenMovimientos.Add(origenmovimiento);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(origenmovimiento).State = EntityState.Detached;
                throw;
            }

            OnAfterOrigenMovimientoCreated(origenmovimiento);

            return origenmovimiento;
        }

        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> CancelOrigenMovimientoChanges(SGPA.Server.Models.CMU.OrigenMovimiento item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnOrigenMovimientoUpdated(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnAfterOrigenMovimientoUpdated(SGPA.Server.Models.CMU.OrigenMovimiento item);

        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> UpdateOrigenMovimiento(int id, SGPA.Server.Models.CMU.OrigenMovimiento origenmovimiento)
        {
            OnOrigenMovimientoUpdated(origenmovimiento);

            var itemToUpdate = Context.OrigenMovimientos
                              .Where(i => i.Id == origenmovimiento.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(origenmovimiento);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterOrigenMovimientoUpdated(origenmovimiento);

            return origenmovimiento;
        }

        partial void OnOrigenMovimientoDeleted(SGPA.Server.Models.CMU.OrigenMovimiento item);
        partial void OnAfterOrigenMovimientoDeleted(SGPA.Server.Models.CMU.OrigenMovimiento item);

        public async Task<SGPA.Server.Models.CMU.OrigenMovimiento> DeleteOrigenMovimiento(int id)
        {
            var itemToDelete = Context.OrigenMovimientos
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzas)
                              .Include(i => i.MovimientoCuenta)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrigenMovimientoDeleted(itemToDelete);


            Context.OrigenMovimientos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOrigenMovimientoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportPaisToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/pais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/pais/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPaisToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/pais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/pais/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPaisRead(ref IQueryable<SGPA.Server.Models.CMU.Pai> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Pai>> GetPais(Query query = null)
        {
            var items = Context.Pais.AsQueryable();


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

            OnPaisRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPaiGet(SGPA.Server.Models.CMU.Pai item);
        partial void OnGetPaiByCodigo(ref IQueryable<SGPA.Server.Models.CMU.Pai> items);


        public async Task<SGPA.Server.Models.CMU.Pai> GetPaiByCodigo(int codigo)
        {
            var items = Context.Pais
                              .AsNoTracking()
                              .Where(i => i.Codigo == codigo);

 
            OnGetPaiByCodigo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPaiGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPaiCreated(SGPA.Server.Models.CMU.Pai item);
        partial void OnAfterPaiCreated(SGPA.Server.Models.CMU.Pai item);

        public async Task<SGPA.Server.Models.CMU.Pai> CreatePai(SGPA.Server.Models.CMU.Pai pai)
        {
            OnPaiCreated(pai);

            var existingItem = Context.Pais
                              .Where(i => i.Codigo == pai.Codigo)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Pais.Add(pai);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(pai).State = EntityState.Detached;
                throw;
            }

            OnAfterPaiCreated(pai);

            return pai;
        }

        public async Task<SGPA.Server.Models.CMU.Pai> CancelPaiChanges(SGPA.Server.Models.CMU.Pai item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPaiUpdated(SGPA.Server.Models.CMU.Pai item);
        partial void OnAfterPaiUpdated(SGPA.Server.Models.CMU.Pai item);

        public async Task<SGPA.Server.Models.CMU.Pai> UpdatePai(int codigo, SGPA.Server.Models.CMU.Pai pai)
        {
            OnPaiUpdated(pai);

            var itemToUpdate = Context.Pais
                              .Where(i => i.Codigo == pai.Codigo)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(pai);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPaiUpdated(pai);

            return pai;
        }

        partial void OnPaiDeleted(SGPA.Server.Models.CMU.Pai item);
        partial void OnAfterPaiDeleted(SGPA.Server.Models.CMU.Pai item);

        public async Task<SGPA.Server.Models.CMU.Pai> DeletePai(int codigo)
        {
            var itemToDelete = Context.Pais
                              .Where(i => i.Codigo == codigo)
                              .Include(i => i.Colegiados)
                              .Include(i => i.RegistroColegiados)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPaiDeleted(itemToDelete);


            Context.Pais.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPaiDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportParametrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/parametros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportParametrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/parametros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnParametrosRead(ref IQueryable<SGPA.Server.Models.CMU.Parametro> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Parametro>> GetParametros(Query query = null)
        {
            var items = Context.Parametros.AsQueryable();

            items = items.Include(i => i.AgenteCobranza);
            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.CategoriaColegiado);
            items = items.Include(i => i.CategoriaColegiado1);
            items = items.Include(i => i.DeclaracionJuradaTipo);
            items = items.Include(i => i.DeclaracionJuradaTipo1);
            items = items.Include(i => i.MovimientoTipo);
            items = items.Include(i => i.MovimientoTipo1);
            items = items.Include(i => i.MovimientoTipo2);
            items = items.Include(i => i.MovimientoTipo3);

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

        partial void OnParametroGet(SGPA.Server.Models.CMU.Parametro item);
        partial void OnGetParametroById(ref IQueryable<SGPA.Server.Models.CMU.Parametro> items);


        public async Task<SGPA.Server.Models.CMU.Parametro> GetParametroById(int id)
        {
            var items = Context.Parametros
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.AgenteCobranza);
            items = items.Include(i => i.AgenteCobranzaDebito);
            items = items.Include(i => i.CategoriaColegiado);
            items = items.Include(i => i.CategoriaColegiado1);
            items = items.Include(i => i.DeclaracionJuradaTipo);
            items = items.Include(i => i.DeclaracionJuradaTipo1);
            items = items.Include(i => i.MovimientoTipo);
            items = items.Include(i => i.MovimientoTipo1);
            items = items.Include(i => i.MovimientoTipo2);
            items = items.Include(i => i.MovimientoTipo3);
 
            OnGetParametroById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnParametroGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnParametroCreated(SGPA.Server.Models.CMU.Parametro item);
        partial void OnAfterParametroCreated(SGPA.Server.Models.CMU.Parametro item);

        public async Task<SGPA.Server.Models.CMU.Parametro> CreateParametro(SGPA.Server.Models.CMU.Parametro parametro)
        {
            OnParametroCreated(parametro);

            var existingItem = Context.Parametros
                              .Where(i => i.Id == parametro.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Parametros.Add(parametro);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(parametro).State = EntityState.Detached;
                throw;
            }

            OnAfterParametroCreated(parametro);

            return parametro;
        }

        public async Task<SGPA.Server.Models.CMU.Parametro> CancelParametroChanges(SGPA.Server.Models.CMU.Parametro item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnParametroUpdated(SGPA.Server.Models.CMU.Parametro item);
        partial void OnAfterParametroUpdated(SGPA.Server.Models.CMU.Parametro item);

        public async Task<SGPA.Server.Models.CMU.Parametro> UpdateParametro(int id, SGPA.Server.Models.CMU.Parametro parametro)
        {
            OnParametroUpdated(parametro);

            var itemToUpdate = Context.Parametros
                              .Where(i => i.Id == parametro.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(parametro);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterParametroUpdated(parametro);

            return parametro;
        }

        partial void OnParametroDeleted(SGPA.Server.Models.CMU.Parametro item);
        partial void OnAfterParametroDeleted(SGPA.Server.Models.CMU.Parametro item);

        public async Task<SGPA.Server.Models.CMU.Parametro> DeleteParametro(int id)
        {
            var itemToDelete = Context.Parametros
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnParametroDeleted(itemToDelete);


            Context.Parametros.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterParametroDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegionsRead(ref IQueryable<SGPA.Server.Models.CMU.Region> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Region>> GetRegions(Query query = null)
        {
            var items = Context.Regions.AsQueryable();


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

            OnRegionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegionGet(SGPA.Server.Models.CMU.Region item);
        partial void OnGetRegionById(ref IQueryable<SGPA.Server.Models.CMU.Region> items);


        public async Task<SGPA.Server.Models.CMU.Region> GetRegionById(int id)
        {
            var items = Context.Regions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetRegionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegionCreated(SGPA.Server.Models.CMU.Region item);
        partial void OnAfterRegionCreated(SGPA.Server.Models.CMU.Region item);

        public async Task<SGPA.Server.Models.CMU.Region> CreateRegion(SGPA.Server.Models.CMU.Region region)
        {
            OnRegionCreated(region);

            var existingItem = Context.Regions
                              .Where(i => i.Id == region.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Regions.Add(region);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(region).State = EntityState.Detached;
                throw;
            }

            OnAfterRegionCreated(region);

            return region;
        }

        public async Task<SGPA.Server.Models.CMU.Region> CancelRegionChanges(SGPA.Server.Models.CMU.Region item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegionUpdated(SGPA.Server.Models.CMU.Region item);
        partial void OnAfterRegionUpdated(SGPA.Server.Models.CMU.Region item);

        public async Task<SGPA.Server.Models.CMU.Region> UpdateRegion(int id, SGPA.Server.Models.CMU.Region region)
        {
            OnRegionUpdated(region);

            var itemToUpdate = Context.Regions
                              .Where(i => i.Id == region.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(region);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegionUpdated(region);

            return region;
        }

        partial void OnRegionDeleted(SGPA.Server.Models.CMU.Region item);
        partial void OnAfterRegionDeleted(SGPA.Server.Models.CMU.Region item);

        public async Task<SGPA.Server.Models.CMU.Region> DeleteRegion(int id)
        {
            var itemToDelete = Context.Regions
                              .Where(i => i.Id == id)
                              .Include(i => i.AgenteCobranzas)
                              .Include(i => i.Regionals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegionDeleted(itemToDelete);


            Context.Regions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegionalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegionalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegionalsRead(ref IQueryable<SGPA.Server.Models.CMU.Regional> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Regional>> GetRegionals(Query query = null)
        {
            var items = Context.Regionals.AsQueryable();

            items = items.Include(i => i.Region1);

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

            OnRegionalsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegionalGet(SGPA.Server.Models.CMU.Regional item);
        partial void OnGetRegionalById(ref IQueryable<SGPA.Server.Models.CMU.Regional> items);


        public async Task<SGPA.Server.Models.CMU.Regional> GetRegionalById(int id)
        {
            var items = Context.Regionals
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Region1);
 
            OnGetRegionalById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegionalGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegionalCreated(SGPA.Server.Models.CMU.Regional item);
        partial void OnAfterRegionalCreated(SGPA.Server.Models.CMU.Regional item);

        public async Task<SGPA.Server.Models.CMU.Regional> CreateRegional(SGPA.Server.Models.CMU.Regional regional)
        {
            OnRegionalCreated(regional);

            var existingItem = Context.Regionals
                              .Where(i => i.Id == regional.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Regionals.Add(regional);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(regional).State = EntityState.Detached;
                throw;
            }

            OnAfterRegionalCreated(regional);

            return regional;
        }

        public async Task<SGPA.Server.Models.CMU.Regional> CancelRegionalChanges(SGPA.Server.Models.CMU.Regional item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegionalUpdated(SGPA.Server.Models.CMU.Regional item);
        partial void OnAfterRegionalUpdated(SGPA.Server.Models.CMU.Regional item);

        public async Task<SGPA.Server.Models.CMU.Regional> UpdateRegional(int id, SGPA.Server.Models.CMU.Regional regional)
        {
            OnRegionalUpdated(regional);

            var itemToUpdate = Context.Regionals
                              .Where(i => i.Id == regional.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(regional);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegionalUpdated(regional);

            return regional;
        }

        partial void OnRegionalDeleted(SGPA.Server.Models.CMU.Regional item);
        partial void OnAfterRegionalDeleted(SGPA.Server.Models.CMU.Regional item);

        public async Task<SGPA.Server.Models.CMU.Regional> DeleteRegional(int id)
        {
            var itemToDelete = Context.Regionals
                              .Where(i => i.Id == id)
                              .Include(i => i.Colegiados)
                              .Include(i => i.Departamentos)
                              .Include(i => i.RegionalregionalesCuentabancariacuentabancaria)
                              .Include(i => i.UsuarioRegionals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegionalDeleted(itemToDelete);


            Context.Regionals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegionalDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegionalregionalesCuentabancariacuentabancariaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionalregionalescuentabancariacuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionalregionalescuentabancariacuentabancaria/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegionalregionalesCuentabancariacuentabancariaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/regionalregionalescuentabancariacuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/regionalregionalescuentabancariacuentabancaria/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaRead(ref IQueryable<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria>> GetRegionalregionalesCuentabancariacuentabancaria(Query query = null)
        {
            var items = Context.RegionalregionalesCuentabancariacuentabancaria.AsQueryable();

            items = items.Include(i => i.CuentaBancarium);
            items = items.Include(i => i.Regional);

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

            OnRegionalregionalesCuentabancariacuentabancariaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaGet(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnGetRegionalregionalesCuentabancariacuentabancariaByOid(ref IQueryable<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> items);


        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> GetRegionalregionalesCuentabancariacuentabancariaByOid(int oid)
        {
            var items = Context.RegionalregionalesCuentabancariacuentabancaria
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.CuentaBancarium);
            items = items.Include(i => i.Regional);
 
            OnGetRegionalregionalesCuentabancariacuentabancariaByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegionalregionalesCuentabancariacuentabancariaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaCreated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnAfterRegionalregionalesCuentabancariacuentabancariaCreated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);

        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> CreateRegionalregionalesCuentabancariacuentabancaria(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria regionalregionalescuentabancariacuentabancaria)
        {
            OnRegionalregionalesCuentabancariacuentabancariaCreated(regionalregionalescuentabancariacuentabancaria);

            var existingItem = Context.RegionalregionalesCuentabancariacuentabancaria
                              .Where(i => i.OID == regionalregionalescuentabancariacuentabancaria.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RegionalregionalesCuentabancariacuentabancaria.Add(regionalregionalescuentabancariacuentabancaria);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(regionalregionalescuentabancariacuentabancaria).State = EntityState.Detached;
                throw;
            }

            OnAfterRegionalregionalesCuentabancariacuentabancariaCreated(regionalregionalescuentabancariacuentabancaria);

            return regionalregionalescuentabancariacuentabancaria;
        }

        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> CancelRegionalregionalesCuentabancariacuentabancariaChanges(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaUpdated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnAfterRegionalregionalesCuentabancariacuentabancariaUpdated(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);

        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> UpdateRegionalregionalesCuentabancariacuentabancaria(int oid, SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria regionalregionalescuentabancariacuentabancaria)
        {
            OnRegionalregionalesCuentabancariacuentabancariaUpdated(regionalregionalescuentabancariacuentabancaria);

            var itemToUpdate = Context.RegionalregionalesCuentabancariacuentabancaria
                              .Where(i => i.OID == regionalregionalescuentabancariacuentabancaria.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(regionalregionalescuentabancariacuentabancaria);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegionalregionalesCuentabancariacuentabancariaUpdated(regionalregionalescuentabancariacuentabancaria);

            return regionalregionalescuentabancariacuentabancaria;
        }

        partial void OnRegionalregionalesCuentabancariacuentabancariaDeleted(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);
        partial void OnAfterRegionalregionalesCuentabancariacuentabancariaDeleted(SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria item);

        public async Task<SGPA.Server.Models.CMU.RegionalregionalesCuentabancariacuentabancaria> DeleteRegionalregionalesCuentabancariacuentabancaria(int oid)
        {
            var itemToDelete = Context.RegionalregionalesCuentabancariacuentabancaria
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegionalregionalesCuentabancariacuentabancariaDeleted(itemToDelete);


            Context.RegionalregionalesCuentabancariacuentabancaria.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegionalregionalesCuentabancariacuentabancariaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegistroColegiadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegistroColegiadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegistroColegiadosRead(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.RegistroColegiado>> GetRegistroColegiados(Query query = null)
        {
            var items = Context.RegistroColegiados.AsQueryable();

            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.Pai);
            items = items.Include(i => i.Universidad);
            items = items.Include(i => i.UniversidadTituloGrado1);

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

            OnRegistroColegiadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegistroColegiadoGet(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnGetRegistroColegiadoByOid(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiado> items);


        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> GetRegistroColegiadoByOid(int oid)
        {
            var items = Context.RegistroColegiados
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.Departamento1);
            items = items.Include(i => i.Pai);
            items = items.Include(i => i.Universidad);
            items = items.Include(i => i.UniversidadTituloGrado1);
 
            OnGetRegistroColegiadoByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegistroColegiadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegistroColegiadoCreated(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnAfterRegistroColegiadoCreated(SGPA.Server.Models.CMU.RegistroColegiado item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> CreateRegistroColegiado(SGPA.Server.Models.CMU.RegistroColegiado registrocolegiado)
        {
            OnRegistroColegiadoCreated(registrocolegiado);

            var existingItem = Context.RegistroColegiados
                              .Where(i => i.OID == registrocolegiado.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RegistroColegiados.Add(registrocolegiado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(registrocolegiado).State = EntityState.Detached;
                throw;
            }

            OnAfterRegistroColegiadoCreated(registrocolegiado);

            return registrocolegiado;
        }

        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> CancelRegistroColegiadoChanges(SGPA.Server.Models.CMU.RegistroColegiado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegistroColegiadoUpdated(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnAfterRegistroColegiadoUpdated(SGPA.Server.Models.CMU.RegistroColegiado item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> UpdateRegistroColegiado(int oid, SGPA.Server.Models.CMU.RegistroColegiado registrocolegiado)
        {
            OnRegistroColegiadoUpdated(registrocolegiado);

            var itemToUpdate = Context.RegistroColegiados
                              .Where(i => i.OID == registrocolegiado.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(registrocolegiado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegistroColegiadoUpdated(registrocolegiado);

            return registrocolegiado;
        }

        partial void OnRegistroColegiadoDeleted(SGPA.Server.Models.CMU.RegistroColegiado item);
        partial void OnAfterRegistroColegiadoDeleted(SGPA.Server.Models.CMU.RegistroColegiado item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiado> DeleteRegistroColegiado(int oid)
        {
            var itemToDelete = Context.RegistroColegiados
                              .Where(i => i.OID == oid)
                              .Include(i => i.RegistroColegiadoNotificacions)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegistroColegiadoDeleted(itemToDelete);


            Context.RegistroColegiados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegistroColegiadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegistroColegiadoNotificacionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadonotificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadonotificacions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegistroColegiadoNotificacionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadonotificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadonotificacions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegistroColegiadoNotificacionsRead(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion>> GetRegistroColegiadoNotificacions(Query query = null)
        {
            var items = Context.RegistroColegiadoNotificacions.AsQueryable();

            items = items.Include(i => i.RegistroColegiado1);

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

            OnRegistroColegiadoNotificacionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegistroColegiadoNotificacionGet(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnGetRegistroColegiadoNotificacionById(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> items);


        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> GetRegistroColegiadoNotificacionById(int id)
        {
            var items = Context.RegistroColegiadoNotificacions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.RegistroColegiado1);
 
            OnGetRegistroColegiadoNotificacionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegistroColegiadoNotificacionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegistroColegiadoNotificacionCreated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnAfterRegistroColegiadoNotificacionCreated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> CreateRegistroColegiadoNotificacion(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion registrocolegiadonotificacion)
        {
            OnRegistroColegiadoNotificacionCreated(registrocolegiadonotificacion);

            var existingItem = Context.RegistroColegiadoNotificacions
                              .Where(i => i.Id == registrocolegiadonotificacion.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RegistroColegiadoNotificacions.Add(registrocolegiadonotificacion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(registrocolegiadonotificacion).State = EntityState.Detached;
                throw;
            }

            OnAfterRegistroColegiadoNotificacionCreated(registrocolegiadonotificacion);

            return registrocolegiadonotificacion;
        }

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> CancelRegistroColegiadoNotificacionChanges(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegistroColegiadoNotificacionUpdated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnAfterRegistroColegiadoNotificacionUpdated(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> UpdateRegistroColegiadoNotificacion(int id, SGPA.Server.Models.CMU.RegistroColegiadoNotificacion registrocolegiadonotificacion)
        {
            OnRegistroColegiadoNotificacionUpdated(registrocolegiadonotificacion);

            var itemToUpdate = Context.RegistroColegiadoNotificacions
                              .Where(i => i.Id == registrocolegiadonotificacion.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(registrocolegiadonotificacion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegistroColegiadoNotificacionUpdated(registrocolegiadonotificacion);

            return registrocolegiadonotificacion;
        }

        partial void OnRegistroColegiadoNotificacionDeleted(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);
        partial void OnAfterRegistroColegiadoNotificacionDeleted(SGPA.Server.Models.CMU.RegistroColegiadoNotificacion item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoNotificacion> DeleteRegistroColegiadoNotificacion(int id)
        {
            var itemToDelete = Context.RegistroColegiadoNotificacions
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegistroColegiadoNotificacionDeleted(itemToDelete);


            Context.RegistroColegiadoNotificacions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegistroColegiadoNotificacionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRegistroColegiadoRechazoParamsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadorechazoparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadorechazoparams/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegistroColegiadoRechazoParamsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/registrocolegiadorechazoparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/registrocolegiadorechazoparams/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegistroColegiadoRechazoParamsRead(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam>> GetRegistroColegiadoRechazoParams(Query query = null)
        {
            var items = Context.RegistroColegiadoRechazoParams.AsQueryable();


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

            OnRegistroColegiadoRechazoParamsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegistroColegiadoRechazoParamGet(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnGetRegistroColegiadoRechazoParamByOid(ref IQueryable<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> items);


        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> GetRegistroColegiadoRechazoParamByOid(int oid)
        {
            var items = Context.RegistroColegiadoRechazoParams
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

 
            OnGetRegistroColegiadoRechazoParamByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRegistroColegiadoRechazoParamGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRegistroColegiadoRechazoParamCreated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnAfterRegistroColegiadoRechazoParamCreated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> CreateRegistroColegiadoRechazoParam(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam registrocolegiadorechazoparam)
        {
            OnRegistroColegiadoRechazoParamCreated(registrocolegiadorechazoparam);

            var existingItem = Context.RegistroColegiadoRechazoParams
                              .Where(i => i.OID == registrocolegiadorechazoparam.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RegistroColegiadoRechazoParams.Add(registrocolegiadorechazoparam);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(registrocolegiadorechazoparam).State = EntityState.Detached;
                throw;
            }

            OnAfterRegistroColegiadoRechazoParamCreated(registrocolegiadorechazoparam);

            return registrocolegiadorechazoparam;
        }

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> CancelRegistroColegiadoRechazoParamChanges(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRegistroColegiadoRechazoParamUpdated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnAfterRegistroColegiadoRechazoParamUpdated(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> UpdateRegistroColegiadoRechazoParam(int oid, SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam registrocolegiadorechazoparam)
        {
            OnRegistroColegiadoRechazoParamUpdated(registrocolegiadorechazoparam);

            var itemToUpdate = Context.RegistroColegiadoRechazoParams
                              .Where(i => i.OID == registrocolegiadorechazoparam.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(registrocolegiadorechazoparam);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRegistroColegiadoRechazoParamUpdated(registrocolegiadorechazoparam);

            return registrocolegiadorechazoparam;
        }

        partial void OnRegistroColegiadoRechazoParamDeleted(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);
        partial void OnAfterRegistroColegiadoRechazoParamDeleted(SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam item);

        public async Task<SGPA.Server.Models.CMU.RegistroColegiadoRechazoParam> DeleteRegistroColegiadoRechazoParam(int oid)
        {
            var itemToDelete = Context.RegistroColegiadoRechazoParams
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegistroColegiadoRechazoParamDeleted(itemToDelete);


            Context.RegistroColegiadoRechazoParams.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRegistroColegiadoRechazoParamDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportReportDataToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdata/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportReportDataToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdata/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnReportDataRead(ref IQueryable<SGPA.Server.Models.CMU.ReportDatum> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ReportDatum>> GetReportData(Query query = null)
        {
            var items = Context.ReportData.AsQueryable();


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

            OnReportDataRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnReportDatumGet(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnGetReportDatumByOid(ref IQueryable<SGPA.Server.Models.CMU.ReportDatum> items);


        public async Task<SGPA.Server.Models.CMU.ReportDatum> GetReportDatumByOid(int oid)
        {
            var items = Context.ReportData
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

 
            OnGetReportDatumByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnReportDatumGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnReportDatumCreated(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnAfterReportDatumCreated(SGPA.Server.Models.CMU.ReportDatum item);

        public async Task<SGPA.Server.Models.CMU.ReportDatum> CreateReportDatum(SGPA.Server.Models.CMU.ReportDatum reportdatum)
        {
            OnReportDatumCreated(reportdatum);

            var existingItem = Context.ReportData
                              .Where(i => i.OID == reportdatum.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ReportData.Add(reportdatum);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(reportdatum).State = EntityState.Detached;
                throw;
            }

            OnAfterReportDatumCreated(reportdatum);

            return reportdatum;
        }

        public async Task<SGPA.Server.Models.CMU.ReportDatum> CancelReportDatumChanges(SGPA.Server.Models.CMU.ReportDatum item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnReportDatumUpdated(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnAfterReportDatumUpdated(SGPA.Server.Models.CMU.ReportDatum item);

        public async Task<SGPA.Server.Models.CMU.ReportDatum> UpdateReportDatum(int oid, SGPA.Server.Models.CMU.ReportDatum reportdatum)
        {
            OnReportDatumUpdated(reportdatum);

            var itemToUpdate = Context.ReportData
                              .Where(i => i.OID == reportdatum.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(reportdatum);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterReportDatumUpdated(reportdatum);

            return reportdatum;
        }

        partial void OnReportDatumDeleted(SGPA.Server.Models.CMU.ReportDatum item);
        partial void OnAfterReportDatumDeleted(SGPA.Server.Models.CMU.ReportDatum item);

        public async Task<SGPA.Server.Models.CMU.ReportDatum> DeleteReportDatum(int oid)
        {
            var itemToDelete = Context.ReportData
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnReportDatumDeleted(itemToDelete);


            Context.ReportData.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterReportDatumDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportReportDataV2SToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdatav2s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdatav2s/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportReportDataV2SToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/reportdatav2s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/reportdatav2s/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnReportDataV2SRead(ref IQueryable<SGPA.Server.Models.CMU.ReportDataV2> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.ReportDataV2>> GetReportDataV2S(Query query = null)
        {
            var items = Context.ReportDataV2S.AsQueryable();


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

            OnReportDataV2SRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnReportDataV2Get(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnGetReportDataV2ByOid(ref IQueryable<SGPA.Server.Models.CMU.ReportDataV2> items);


        public async Task<SGPA.Server.Models.CMU.ReportDataV2> GetReportDataV2ByOid(Guid oid)
        {
            var items = Context.ReportDataV2S
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetReportDataV2ByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnReportDataV2Get(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnReportDataV2Created(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnAfterReportDataV2Created(SGPA.Server.Models.CMU.ReportDataV2 item);

        public async Task<SGPA.Server.Models.CMU.ReportDataV2> CreateReportDataV2(SGPA.Server.Models.CMU.ReportDataV2 reportdatav2)
        {
            OnReportDataV2Created(reportdatav2);

            var existingItem = Context.ReportDataV2S
                              .Where(i => i.Oid == reportdatav2.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ReportDataV2S.Add(reportdatav2);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(reportdatav2).State = EntityState.Detached;
                throw;
            }

            OnAfterReportDataV2Created(reportdatav2);

            return reportdatav2;
        }

        public async Task<SGPA.Server.Models.CMU.ReportDataV2> CancelReportDataV2Changes(SGPA.Server.Models.CMU.ReportDataV2 item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnReportDataV2Updated(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnAfterReportDataV2Updated(SGPA.Server.Models.CMU.ReportDataV2 item);

        public async Task<SGPA.Server.Models.CMU.ReportDataV2> UpdateReportDataV2(Guid oid, SGPA.Server.Models.CMU.ReportDataV2 reportdatav2)
        {
            OnReportDataV2Updated(reportdatav2);

            var itemToUpdate = Context.ReportDataV2S
                              .Where(i => i.Oid == reportdatav2.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(reportdatav2);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterReportDataV2Updated(reportdatav2);

            return reportdatav2;
        }

        partial void OnReportDataV2Deleted(SGPA.Server.Models.CMU.ReportDataV2 item);
        partial void OnAfterReportDataV2Deleted(SGPA.Server.Models.CMU.ReportDataV2 item);

        public async Task<SGPA.Server.Models.CMU.ReportDataV2> DeleteReportDataV2(Guid oid)
        {
            var itemToDelete = Context.ReportDataV2S
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnReportDataV2Deleted(itemToDelete);


            Context.ReportDataV2S.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterReportDataV2Deleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRolsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rols/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rols/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolsRead(ref IQueryable<SGPA.Server.Models.CMU.Rol> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Rol>> GetRols(Query query = null)
        {
            var items = Context.Rols.AsQueryable();

            items = items.Include(i => i.SecuritySystemRole);

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

            OnRolsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRolGet(SGPA.Server.Models.CMU.Rol item);
        partial void OnGetRolByOid(ref IQueryable<SGPA.Server.Models.CMU.Rol> items);


        public async Task<SGPA.Server.Models.CMU.Rol> GetRolByOid(Guid oid)
        {
            var items = Context.Rols
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.SecuritySystemRole);
 
            OnGetRolByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRolGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRolCreated(SGPA.Server.Models.CMU.Rol item);
        partial void OnAfterRolCreated(SGPA.Server.Models.CMU.Rol item);

        public async Task<SGPA.Server.Models.CMU.Rol> CreateRol(SGPA.Server.Models.CMU.Rol rol)
        {
            OnRolCreated(rol);

            var existingItem = Context.Rols
                              .Where(i => i.Oid == rol.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Rols.Add(rol);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(rol).State = EntityState.Detached;
                throw;
            }

            OnAfterRolCreated(rol);

            return rol;
        }

        public async Task<SGPA.Server.Models.CMU.Rol> CancelRolChanges(SGPA.Server.Models.CMU.Rol item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRolUpdated(SGPA.Server.Models.CMU.Rol item);
        partial void OnAfterRolUpdated(SGPA.Server.Models.CMU.Rol item);

        public async Task<SGPA.Server.Models.CMU.Rol> UpdateRol(Guid oid, SGPA.Server.Models.CMU.Rol rol)
        {
            OnRolUpdated(rol);

            var itemToUpdate = Context.Rols
                              .Where(i => i.Oid == rol.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(rol);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRolUpdated(rol);

            return rol;
        }

        partial void OnRolDeleted(SGPA.Server.Models.CMU.Rol item);
        partial void OnAfterRolDeleted(SGPA.Server.Models.CMU.Rol item);

        public async Task<SGPA.Server.Models.CMU.Rol> DeleteRol(Guid oid)
        {
            var itemToDelete = Context.Rols
                              .Where(i => i.Oid == oid)
                              .Include(i => i.RolrolesMovimientotipomovimientostipos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRolDeleted(itemToDelete);


            Context.Rols.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRolDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportRolrolesMovimientotipomovimientostiposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rolrolesmovimientotipomovimientostipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rolrolesmovimientotipomovimientostipos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolrolesMovimientotipomovimientostiposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/rolrolesmovimientotipomovimientostipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/rolrolesmovimientotipomovimientostipos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolrolesMovimientotipomovimientostiposRead(ref IQueryable<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo>> GetRolrolesMovimientotipomovimientostipos(Query query = null)
        {
            var items = Context.RolrolesMovimientotipomovimientostipos.AsQueryable();

            items = items.Include(i => i.MovimientoTipo);
            items = items.Include(i => i.Rol);

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

            OnRolrolesMovimientotipomovimientostiposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRolrolesMovimientotipomovimientostipoGet(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnGetRolrolesMovimientotipomovimientostipoByOid(ref IQueryable<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> items);


        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> GetRolrolesMovimientotipomovimientostipoByOid(int oid)
        {
            var items = Context.RolrolesMovimientotipomovimientostipos
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.MovimientoTipo);
            items = items.Include(i => i.Rol);
 
            OnGetRolrolesMovimientotipomovimientostipoByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnRolrolesMovimientotipomovimientostipoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnRolrolesMovimientotipomovimientostipoCreated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnAfterRolrolesMovimientotipomovimientostipoCreated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);

        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> CreateRolrolesMovimientotipomovimientostipo(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo rolrolesmovimientotipomovimientostipo)
        {
            OnRolrolesMovimientotipomovimientostipoCreated(rolrolesmovimientotipomovimientostipo);

            var existingItem = Context.RolrolesMovimientotipomovimientostipos
                              .Where(i => i.OID == rolrolesmovimientotipomovimientostipo.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.RolrolesMovimientotipomovimientostipos.Add(rolrolesmovimientotipomovimientostipo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(rolrolesmovimientotipomovimientostipo).State = EntityState.Detached;
                throw;
            }

            OnAfterRolrolesMovimientotipomovimientostipoCreated(rolrolesmovimientotipomovimientostipo);

            return rolrolesmovimientotipomovimientostipo;
        }

        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> CancelRolrolesMovimientotipomovimientostipoChanges(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnRolrolesMovimientotipomovimientostipoUpdated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnAfterRolrolesMovimientotipomovimientostipoUpdated(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);

        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> UpdateRolrolesMovimientotipomovimientostipo(int oid, SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo rolrolesmovimientotipomovimientostipo)
        {
            OnRolrolesMovimientotipomovimientostipoUpdated(rolrolesmovimientotipomovimientostipo);

            var itemToUpdate = Context.RolrolesMovimientotipomovimientostipos
                              .Where(i => i.OID == rolrolesmovimientotipomovimientostipo.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(rolrolesmovimientotipomovimientostipo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterRolrolesMovimientotipomovimientostipoUpdated(rolrolesmovimientotipomovimientostipo);

            return rolrolesmovimientotipomovimientostipo;
        }

        partial void OnRolrolesMovimientotipomovimientostipoDeleted(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);
        partial void OnAfterRolrolesMovimientotipomovimientostipoDeleted(SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo item);

        public async Task<SGPA.Server.Models.CMU.RolrolesMovimientotipomovimientostipo> DeleteRolrolesMovimientotipomovimientostipo(int oid)
        {
            var itemToDelete = Context.RolrolesMovimientotipomovimientostipos
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRolrolesMovimientotipomovimientostipoDeleted(itemToDelete);


            Context.RolrolesMovimientotipomovimientostipos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRolrolesMovimientotipomovimientostipoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalaCmusToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salacmus/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salacmus/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalaCmusToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salacmus/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salacmus/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalaCmusRead(ref IQueryable<SGPA.Server.Models.CMU.SalaCmu> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SalaCmu>> GetSalaCmus(Query query = null)
        {
            var items = Context.SalaCmus.AsQueryable();


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

            OnSalaCmusRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalaCmuGet(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnGetSalaCmuById(ref IQueryable<SGPA.Server.Models.CMU.SalaCmu> items);


        public async Task<SGPA.Server.Models.CMU.SalaCmu> GetSalaCmuById(int id)
        {
            var items = Context.SalaCmus
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetSalaCmuById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalaCmuGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalaCmuCreated(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnAfterSalaCmuCreated(SGPA.Server.Models.CMU.SalaCmu item);

        public async Task<SGPA.Server.Models.CMU.SalaCmu> CreateSalaCmu(SGPA.Server.Models.CMU.SalaCmu salacmu)
        {
            OnSalaCmuCreated(salacmu);

            var existingItem = Context.SalaCmus
                              .Where(i => i.Id == salacmu.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalaCmus.Add(salacmu);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salacmu).State = EntityState.Detached;
                throw;
            }

            OnAfterSalaCmuCreated(salacmu);

            return salacmu;
        }

        public async Task<SGPA.Server.Models.CMU.SalaCmu> CancelSalaCmuChanges(SGPA.Server.Models.CMU.SalaCmu item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalaCmuUpdated(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnAfterSalaCmuUpdated(SGPA.Server.Models.CMU.SalaCmu item);

        public async Task<SGPA.Server.Models.CMU.SalaCmu> UpdateSalaCmu(int id, SGPA.Server.Models.CMU.SalaCmu salacmu)
        {
            OnSalaCmuUpdated(salacmu);

            var itemToUpdate = Context.SalaCmus
                              .Where(i => i.Id == salacmu.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salacmu);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalaCmuUpdated(salacmu);

            return salacmu;
        }

        partial void OnSalaCmuDeleted(SGPA.Server.Models.CMU.SalaCmu item);
        partial void OnAfterSalaCmuDeleted(SGPA.Server.Models.CMU.SalaCmu item);

        public async Task<SGPA.Server.Models.CMU.SalaCmu> DeleteSalaCmu(int id)
        {
            var itemToDelete = Context.SalaCmus
                              .Where(i => i.Id == id)
                              .Include(i => i.SalaReservas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalaCmuDeleted(itemToDelete);


            Context.SalaCmus.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalaCmuDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalaOrganizadorsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salaorganizadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salaorganizadors/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalaOrganizadorsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salaorganizadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salaorganizadors/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalaOrganizadorsRead(ref IQueryable<SGPA.Server.Models.CMU.SalaOrganizador> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SalaOrganizador>> GetSalaOrganizadors(Query query = null)
        {
            var items = Context.SalaOrganizadors.AsQueryable();


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

            OnSalaOrganizadorsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalaOrganizadorGet(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnGetSalaOrganizadorById(ref IQueryable<SGPA.Server.Models.CMU.SalaOrganizador> items);


        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> GetSalaOrganizadorById(int id)
        {
            var items = Context.SalaOrganizadors
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetSalaOrganizadorById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalaOrganizadorGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalaOrganizadorCreated(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnAfterSalaOrganizadorCreated(SGPA.Server.Models.CMU.SalaOrganizador item);

        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> CreateSalaOrganizador(SGPA.Server.Models.CMU.SalaOrganizador salaorganizador)
        {
            OnSalaOrganizadorCreated(salaorganizador);

            var existingItem = Context.SalaOrganizadors
                              .Where(i => i.Id == salaorganizador.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalaOrganizadors.Add(salaorganizador);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salaorganizador).State = EntityState.Detached;
                throw;
            }

            OnAfterSalaOrganizadorCreated(salaorganizador);

            return salaorganizador;
        }

        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> CancelSalaOrganizadorChanges(SGPA.Server.Models.CMU.SalaOrganizador item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalaOrganizadorUpdated(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnAfterSalaOrganizadorUpdated(SGPA.Server.Models.CMU.SalaOrganizador item);

        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> UpdateSalaOrganizador(int id, SGPA.Server.Models.CMU.SalaOrganizador salaorganizador)
        {
            OnSalaOrganizadorUpdated(salaorganizador);

            var itemToUpdate = Context.SalaOrganizadors
                              .Where(i => i.Id == salaorganizador.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salaorganizador);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalaOrganizadorUpdated(salaorganizador);

            return salaorganizador;
        }

        partial void OnSalaOrganizadorDeleted(SGPA.Server.Models.CMU.SalaOrganizador item);
        partial void OnAfterSalaOrganizadorDeleted(SGPA.Server.Models.CMU.SalaOrganizador item);

        public async Task<SGPA.Server.Models.CMU.SalaOrganizador> DeleteSalaOrganizador(int id)
        {
            var itemToDelete = Context.SalaOrganizadors
                              .Where(i => i.Id == id)
                              .Include(i => i.SalaReservas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalaOrganizadorDeleted(itemToDelete);


            Context.SalaOrganizadors.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalaOrganizadorDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalaReservasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalaReservasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalaReservasRead(ref IQueryable<SGPA.Server.Models.CMU.SalaReserva> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SalaReserva>> GetSalaReservas(Query query = null)
        {
            var items = Context.SalaReservas.AsQueryable();

            items = items.Include(i => i.MyFileDatum);
            items = items.Include(i => i.SalaOrganizador);
            items = items.Include(i => i.SalaCmu);

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

            OnSalaReservasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalaReservaGet(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnGetSalaReservaById(ref IQueryable<SGPA.Server.Models.CMU.SalaReserva> items);


        public async Task<SGPA.Server.Models.CMU.SalaReserva> GetSalaReservaById(int id)
        {
            var items = Context.SalaReservas
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.MyFileDatum);
            items = items.Include(i => i.SalaOrganizador);
            items = items.Include(i => i.SalaCmu);
 
            OnGetSalaReservaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalaReservaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalaReservaCreated(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnAfterSalaReservaCreated(SGPA.Server.Models.CMU.SalaReserva item);

        public async Task<SGPA.Server.Models.CMU.SalaReserva> CreateSalaReserva(SGPA.Server.Models.CMU.SalaReserva salareserva)
        {
            OnSalaReservaCreated(salareserva);

            var existingItem = Context.SalaReservas
                              .Where(i => i.Id == salareserva.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalaReservas.Add(salareserva);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salareserva).State = EntityState.Detached;
                throw;
            }

            OnAfterSalaReservaCreated(salareserva);

            return salareserva;
        }

        public async Task<SGPA.Server.Models.CMU.SalaReserva> CancelSalaReservaChanges(SGPA.Server.Models.CMU.SalaReserva item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalaReservaUpdated(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnAfterSalaReservaUpdated(SGPA.Server.Models.CMU.SalaReserva item);

        public async Task<SGPA.Server.Models.CMU.SalaReserva> UpdateSalaReserva(int id, SGPA.Server.Models.CMU.SalaReserva salareserva)
        {
            OnSalaReservaUpdated(salareserva);

            var itemToUpdate = Context.SalaReservas
                              .Where(i => i.Id == salareserva.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salareserva);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalaReservaUpdated(salareserva);

            return salareserva;
        }

        partial void OnSalaReservaDeleted(SGPA.Server.Models.CMU.SalaReserva item);
        partial void OnAfterSalaReservaDeleted(SGPA.Server.Models.CMU.SalaReserva item);

        public async Task<SGPA.Server.Models.CMU.SalaReserva> DeleteSalaReserva(int id)
        {
            var itemToDelete = Context.SalaReservas
                              .Where(i => i.Id == id)
                              .Include(i => i.SalaReservaRegistros)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalaReservaDeleted(itemToDelete);


            Context.SalaReservas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalaReservaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSalaReservaRegistrosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservaregistros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservaregistros/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSalaReservaRegistrosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/salareservaregistros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/salareservaregistros/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSalaReservaRegistrosRead(ref IQueryable<SGPA.Server.Models.CMU.SalaReservaRegistro> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SalaReservaRegistro>> GetSalaReservaRegistros(Query query = null)
        {
            var items = Context.SalaReservaRegistros.AsQueryable();

            items = items.Include(i => i.SalaReserva);

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

            OnSalaReservaRegistrosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSalaReservaRegistroGet(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnGetSalaReservaRegistroById(ref IQueryable<SGPA.Server.Models.CMU.SalaReservaRegistro> items);


        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> GetSalaReservaRegistroById(int id)
        {
            var items = Context.SalaReservaRegistros
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.SalaReserva);
 
            OnGetSalaReservaRegistroById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSalaReservaRegistroGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSalaReservaRegistroCreated(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnAfterSalaReservaRegistroCreated(SGPA.Server.Models.CMU.SalaReservaRegistro item);

        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> CreateSalaReservaRegistro(SGPA.Server.Models.CMU.SalaReservaRegistro salareservaregistro)
        {
            OnSalaReservaRegistroCreated(salareservaregistro);

            var existingItem = Context.SalaReservaRegistros
                              .Where(i => i.Id == salareservaregistro.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SalaReservaRegistros.Add(salareservaregistro);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(salareservaregistro).State = EntityState.Detached;
                throw;
            }

            OnAfterSalaReservaRegistroCreated(salareservaregistro);

            return salareservaregistro;
        }

        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> CancelSalaReservaRegistroChanges(SGPA.Server.Models.CMU.SalaReservaRegistro item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSalaReservaRegistroUpdated(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnAfterSalaReservaRegistroUpdated(SGPA.Server.Models.CMU.SalaReservaRegistro item);

        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> UpdateSalaReservaRegistro(int id, SGPA.Server.Models.CMU.SalaReservaRegistro salareservaregistro)
        {
            OnSalaReservaRegistroUpdated(salareservaregistro);

            var itemToUpdate = Context.SalaReservaRegistros
                              .Where(i => i.Id == salareservaregistro.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(salareservaregistro);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSalaReservaRegistroUpdated(salareservaregistro);

            return salareservaregistro;
        }

        partial void OnSalaReservaRegistroDeleted(SGPA.Server.Models.CMU.SalaReservaRegistro item);
        partial void OnAfterSalaReservaRegistroDeleted(SGPA.Server.Models.CMU.SalaReservaRegistro item);

        public async Task<SGPA.Server.Models.CMU.SalaReservaRegistro> DeleteSalaReservaRegistro(int id)
        {
            var itemToDelete = Context.SalaReservaRegistros
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSalaReservaRegistroDeleted(itemToDelete);


            Context.SalaReservaRegistros.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSalaReservaRegistroDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritySystemMemberPermissionsObjectsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemmemberpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemmemberpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritySystemMemberPermissionsObjectsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemmemberpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemmemberpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritySystemMemberPermissionsObjectsRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject>> GetSecuritySystemMemberPermissionsObjects(Query query = null)
        {
            var items = Context.SecuritySystemMemberPermissionsObjects.AsQueryable();

            items = items.Include(i => i.SecuritySystemTypePermissionsObject);

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

            OnSecuritySystemMemberPermissionsObjectsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritySystemMemberPermissionsObjectGet(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnGetSecuritySystemMemberPermissionsObjectByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> items);


        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> GetSecuritySystemMemberPermissionsObjectByOid(Guid oid)
        {
            var items = Context.SecuritySystemMemberPermissionsObjects
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.SecuritySystemTypePermissionsObject);
 
            OnGetSecuritySystemMemberPermissionsObjectByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritySystemMemberPermissionsObjectGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritySystemMemberPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnAfterSecuritySystemMemberPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> CreateSecuritySystemMemberPermissionsObject(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject securitysystemmemberpermissionsobject)
        {
            OnSecuritySystemMemberPermissionsObjectCreated(securitysystemmemberpermissionsobject);

            var existingItem = Context.SecuritySystemMemberPermissionsObjects
                              .Where(i => i.Oid == securitysystemmemberpermissionsobject.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritySystemMemberPermissionsObjects.Add(securitysystemmemberpermissionsobject);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemmemberpermissionsobject).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritySystemMemberPermissionsObjectCreated(securitysystemmemberpermissionsobject);

            return securitysystemmemberpermissionsobject;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> CancelSecuritySystemMemberPermissionsObjectChanges(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritySystemMemberPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnAfterSecuritySystemMemberPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> UpdateSecuritySystemMemberPermissionsObject(Guid oid, SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject securitysystemmemberpermissionsobject)
        {
            OnSecuritySystemMemberPermissionsObjectUpdated(securitysystemmemberpermissionsobject);

            var itemToUpdate = Context.SecuritySystemMemberPermissionsObjects
                              .Where(i => i.Oid == securitysystemmemberpermissionsobject.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemmemberpermissionsobject);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritySystemMemberPermissionsObjectUpdated(securitysystemmemberpermissionsobject);

            return securitysystemmemberpermissionsobject;
        }

        partial void OnSecuritySystemMemberPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);
        partial void OnAfterSecuritySystemMemberPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemMemberPermissionsObject> DeleteSecuritySystemMemberPermissionsObject(Guid oid)
        {
            var itemToDelete = Context.SecuritySystemMemberPermissionsObjects
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritySystemMemberPermissionsObjectDeleted(itemToDelete);


            Context.SecuritySystemMemberPermissionsObjects.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritySystemMemberPermissionsObjectDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritySystemObjectPermissionsObjectsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemobjectpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemobjectpermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritySystemObjectPermissionsObjectsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemobjectpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemobjectpermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritySystemObjectPermissionsObjectsRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject>> GetSecuritySystemObjectPermissionsObjects(Query query = null)
        {
            var items = Context.SecuritySystemObjectPermissionsObjects.AsQueryable();

            items = items.Include(i => i.SecuritySystemTypePermissionsObject);

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

            OnSecuritySystemObjectPermissionsObjectsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritySystemObjectPermissionsObjectGet(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnGetSecuritySystemObjectPermissionsObjectByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> items);


        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> GetSecuritySystemObjectPermissionsObjectByOid(Guid oid)
        {
            var items = Context.SecuritySystemObjectPermissionsObjects
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.SecuritySystemTypePermissionsObject);
 
            OnGetSecuritySystemObjectPermissionsObjectByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritySystemObjectPermissionsObjectGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritySystemObjectPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnAfterSecuritySystemObjectPermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> CreateSecuritySystemObjectPermissionsObject(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject securitysystemobjectpermissionsobject)
        {
            OnSecuritySystemObjectPermissionsObjectCreated(securitysystemobjectpermissionsobject);

            var existingItem = Context.SecuritySystemObjectPermissionsObjects
                              .Where(i => i.Oid == securitysystemobjectpermissionsobject.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritySystemObjectPermissionsObjects.Add(securitysystemobjectpermissionsobject);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemobjectpermissionsobject).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritySystemObjectPermissionsObjectCreated(securitysystemobjectpermissionsobject);

            return securitysystemobjectpermissionsobject;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> CancelSecuritySystemObjectPermissionsObjectChanges(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritySystemObjectPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnAfterSecuritySystemObjectPermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> UpdateSecuritySystemObjectPermissionsObject(Guid oid, SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject securitysystemobjectpermissionsobject)
        {
            OnSecuritySystemObjectPermissionsObjectUpdated(securitysystemobjectpermissionsobject);

            var itemToUpdate = Context.SecuritySystemObjectPermissionsObjects
                              .Where(i => i.Oid == securitysystemobjectpermissionsobject.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemobjectpermissionsobject);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritySystemObjectPermissionsObjectUpdated(securitysystemobjectpermissionsobject);

            return securitysystemobjectpermissionsobject;
        }

        partial void OnSecuritySystemObjectPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);
        partial void OnAfterSecuritySystemObjectPermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemObjectPermissionsObject> DeleteSecuritySystemObjectPermissionsObject(Guid oid)
        {
            var itemToDelete = Context.SecuritySystemObjectPermissionsObjects
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritySystemObjectPermissionsObjectDeleted(itemToDelete);


            Context.SecuritySystemObjectPermissionsObjects.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritySystemObjectPermissionsObjectDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritySystemRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritySystemRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritySystemRolesRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemRole> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritySystemRole>> GetSecuritySystemRoles(Query query = null)
        {
            var items = Context.SecuritySystemRoles.AsQueryable();


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

            OnSecuritySystemRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritySystemRoleGet(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnGetSecuritySystemRoleByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemRole> items);


        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> GetSecuritySystemRoleByOid(Guid oid)
        {
            var items = Context.SecuritySystemRoles
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetSecuritySystemRoleByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritySystemRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritySystemRoleCreated(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnAfterSecuritySystemRoleCreated(SGPA.Server.Models.CMU.SecuritySystemRole item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> CreateSecuritySystemRole(SGPA.Server.Models.CMU.SecuritySystemRole securitysystemrole)
        {
            OnSecuritySystemRoleCreated(securitysystemrole);

            var existingItem = Context.SecuritySystemRoles
                              .Where(i => i.Oid == securitysystemrole.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritySystemRoles.Add(securitysystemrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemrole).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritySystemRoleCreated(securitysystemrole);

            return securitysystemrole;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> CancelSecuritySystemRoleChanges(SGPA.Server.Models.CMU.SecuritySystemRole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritySystemRoleUpdated(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnAfterSecuritySystemRoleUpdated(SGPA.Server.Models.CMU.SecuritySystemRole item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> UpdateSecuritySystemRole(Guid oid, SGPA.Server.Models.CMU.SecuritySystemRole securitysystemrole)
        {
            OnSecuritySystemRoleUpdated(securitysystemrole);

            var itemToUpdate = Context.SecuritySystemRoles
                              .Where(i => i.Oid == securitysystemrole.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritySystemRoleUpdated(securitysystemrole);

            return securitysystemrole;
        }

        partial void OnSecuritySystemRoleDeleted(SGPA.Server.Models.CMU.SecuritySystemRole item);
        partial void OnAfterSecuritySystemRoleDeleted(SGPA.Server.Models.CMU.SecuritySystemRole item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemRole> DeleteSecuritySystemRole(Guid oid)
        {
            var itemToDelete = Context.SecuritySystemRoles
                              .Where(i => i.Oid == oid)
                              .Include(i => i.Rols)
                              .Include(i => i.SecuritysystemroleparentrolesSecuritysystemrolechildroles)
                              .Include(i => i.SecuritysystemroleparentrolesSecuritysystemrolechildroles1)
                              .Include(i => i.SecuritySystemTypePermissionsObjects)
                              .Include(i => i.SecuritysystemuserusersSecuritysystemroleroles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritySystemRoleDeleted(itemToDelete);


            Context.SecuritySystemRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritySystemRoleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritysystemroleparentrolesSecuritysystemrolechildrolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritysystemroleparentrolesSecuritysystemrolechildrolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemroleparentrolessecuritysystemrolechildroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildrolesRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole>> GetSecuritysystemroleparentrolesSecuritysystemrolechildroles(Query query = null)
        {
            var items = Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.AsQueryable();

            items = items.Include(i => i.SecuritySystemRole);
            items = items.Include(i => i.SecuritySystemRole1);

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

            OnSecuritysystemroleparentrolesSecuritysystemrolechildrolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleGet(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnGetSecuritysystemroleparentrolesSecuritysystemrolechildroleByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> items);


        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> GetSecuritysystemroleparentrolesSecuritysystemrolechildroleByOid(Guid oid)
        {
            var items = Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.SecuritySystemRole);
            items = items.Include(i => i.SecuritySystemRole1);
 
            OnGetSecuritysystemroleparentrolesSecuritysystemrolechildroleByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritysystemroleparentrolesSecuritysystemrolechildroleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> CreateSecuritysystemroleparentrolesSecuritysystemrolechildrole(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole securitysystemroleparentrolessecuritysystemrolechildrole)
        {
            OnSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(securitysystemroleparentrolessecuritysystemrolechildrole);

            var existingItem = Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                              .Where(i => i.OID == securitysystemroleparentrolessecuritysystemrolechildrole.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Add(securitysystemroleparentrolessecuritysystemrolechildrole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemroleparentrolessecuritysystemrolechildrole).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleCreated(securitysystemroleparentrolessecuritysystemrolechildrole);

            return securitysystemroleparentrolessecuritysystemrolechildrole;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> CancelSecuritysystemroleparentrolesSecuritysystemrolechildroleChanges(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> UpdateSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid oid, SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole securitysystemroleparentrolessecuritysystemrolechildrole)
        {
            OnSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(securitysystemroleparentrolessecuritysystemrolechildrole);

            var itemToUpdate = Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                              .Where(i => i.OID == securitysystemroleparentrolessecuritysystemrolechildrole.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemroleparentrolessecuritysystemrolechildrole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleUpdated(securitysystemroleparentrolessecuritysystemrolechildrole);

            return securitysystemroleparentrolessecuritysystemrolechildrole;
        }

        partial void OnSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);
        partial void OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole item);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemroleparentrolesSecuritysystemrolechildrole> DeleteSecuritysystemroleparentrolesSecuritysystemrolechildrole(Guid oid)
        {
            var itemToDelete = Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(itemToDelete);


            Context.SecuritysystemroleparentrolesSecuritysystemrolechildroles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritysystemroleparentrolesSecuritysystemrolechildroleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritySystemTypePermissionsObjectsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemtypepermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemtypepermissionsobjects/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritySystemTypePermissionsObjectsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemtypepermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemtypepermissionsobjects/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritySystemTypePermissionsObjectsRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject>> GetSecuritySystemTypePermissionsObjects(Query query = null)
        {
            var items = Context.SecuritySystemTypePermissionsObjects.AsQueryable();

            items = items.Include(i => i.SecuritySystemRole);

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

            OnSecuritySystemTypePermissionsObjectsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritySystemTypePermissionsObjectGet(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnGetSecuritySystemTypePermissionsObjectByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> items);


        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> GetSecuritySystemTypePermissionsObjectByOid(Guid oid)
        {
            var items = Context.SecuritySystemTypePermissionsObjects
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.SecuritySystemRole);
 
            OnGetSecuritySystemTypePermissionsObjectByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritySystemTypePermissionsObjectGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritySystemTypePermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnAfterSecuritySystemTypePermissionsObjectCreated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> CreateSecuritySystemTypePermissionsObject(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject securitysystemtypepermissionsobject)
        {
            OnSecuritySystemTypePermissionsObjectCreated(securitysystemtypepermissionsobject);

            var existingItem = Context.SecuritySystemTypePermissionsObjects
                              .Where(i => i.Oid == securitysystemtypepermissionsobject.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritySystemTypePermissionsObjects.Add(securitysystemtypepermissionsobject);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemtypepermissionsobject).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritySystemTypePermissionsObjectCreated(securitysystemtypepermissionsobject);

            return securitysystemtypepermissionsobject;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> CancelSecuritySystemTypePermissionsObjectChanges(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritySystemTypePermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnAfterSecuritySystemTypePermissionsObjectUpdated(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> UpdateSecuritySystemTypePermissionsObject(Guid oid, SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject securitysystemtypepermissionsobject)
        {
            OnSecuritySystemTypePermissionsObjectUpdated(securitysystemtypepermissionsobject);

            var itemToUpdate = Context.SecuritySystemTypePermissionsObjects
                              .Where(i => i.Oid == securitysystemtypepermissionsobject.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemtypepermissionsobject);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritySystemTypePermissionsObjectUpdated(securitysystemtypepermissionsobject);

            return securitysystemtypepermissionsobject;
        }

        partial void OnSecuritySystemTypePermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);
        partial void OnAfterSecuritySystemTypePermissionsObjectDeleted(SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemTypePermissionsObject> DeleteSecuritySystemTypePermissionsObject(Guid oid)
        {
            var itemToDelete = Context.SecuritySystemTypePermissionsObjects
                              .Where(i => i.Oid == oid)
                              .Include(i => i.SecuritySystemMemberPermissionsObjects)
                              .Include(i => i.SecuritySystemObjectPermissionsObjects)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritySystemTypePermissionsObjectDeleted(itemToDelete);


            Context.SecuritySystemTypePermissionsObjects.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritySystemTypePermissionsObjectDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritySystemUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemusers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritySystemUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemusers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritySystemUsersRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemUser> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritySystemUser>> GetSecuritySystemUsers(Query query = null)
        {
            var items = Context.SecuritySystemUsers.AsQueryable();


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

            OnSecuritySystemUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritySystemUserGet(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnGetSecuritySystemUserByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritySystemUser> items);


        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> GetSecuritySystemUserByOid(Guid oid)
        {
            var items = Context.SecuritySystemUsers
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetSecuritySystemUserByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritySystemUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritySystemUserCreated(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnAfterSecuritySystemUserCreated(SGPA.Server.Models.CMU.SecuritySystemUser item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> CreateSecuritySystemUser(SGPA.Server.Models.CMU.SecuritySystemUser securitysystemuser)
        {
            OnSecuritySystemUserCreated(securitysystemuser);

            var existingItem = Context.SecuritySystemUsers
                              .Where(i => i.Oid == securitysystemuser.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritySystemUsers.Add(securitysystemuser);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemuser).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritySystemUserCreated(securitysystemuser);

            return securitysystemuser;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> CancelSecuritySystemUserChanges(SGPA.Server.Models.CMU.SecuritySystemUser item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritySystemUserUpdated(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnAfterSecuritySystemUserUpdated(SGPA.Server.Models.CMU.SecuritySystemUser item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> UpdateSecuritySystemUser(Guid oid, SGPA.Server.Models.CMU.SecuritySystemUser securitysystemuser)
        {
            OnSecuritySystemUserUpdated(securitysystemuser);

            var itemToUpdate = Context.SecuritySystemUsers
                              .Where(i => i.Oid == securitysystemuser.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemuser);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritySystemUserUpdated(securitysystemuser);

            return securitysystemuser;
        }

        partial void OnSecuritySystemUserDeleted(SGPA.Server.Models.CMU.SecuritySystemUser item);
        partial void OnAfterSecuritySystemUserDeleted(SGPA.Server.Models.CMU.SecuritySystemUser item);

        public async Task<SGPA.Server.Models.CMU.SecuritySystemUser> DeleteSecuritySystemUser(Guid oid)
        {
            var itemToDelete = Context.SecuritySystemUsers
                              .Where(i => i.Oid == oid)
                              .Include(i => i.SecuritysystemuserusersSecuritysystemroleroles)
                              .Include(i => i.Usuarios)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritySystemUserDeleted(itemToDelete);


            Context.SecuritySystemUsers.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritySystemUserDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSecuritysystemuserusersSecuritysystemrolerolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemuseruserssecuritysystemroleroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemuseruserssecuritysystemroleroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSecuritysystemuserusersSecuritysystemrolerolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/securitysystemuseruserssecuritysystemroleroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/securitysystemuseruserssecuritysystemroleroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSecuritysystemuserusersSecuritysystemrolerolesRead(ref IQueryable<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole>> GetSecuritysystemuserusersSecuritysystemroleroles(Query query = null)
        {
            var items = Context.SecuritysystemuserusersSecuritysystemroleroles.AsQueryable();

            items = items.Include(i => i.SecuritySystemRole);
            items = items.Include(i => i.SecuritySystemUser);

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

            OnSecuritysystemuserusersSecuritysystemrolerolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSecuritysystemuserusersSecuritysystemroleroleGet(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnGetSecuritysystemuserusersSecuritysystemroleroleByOid(ref IQueryable<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> items);


        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> GetSecuritysystemuserusersSecuritysystemroleroleByOid(Guid oid)
        {
            var items = Context.SecuritysystemuserusersSecuritysystemroleroles
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.SecuritySystemRole);
            items = items.Include(i => i.SecuritySystemUser);
 
            OnGetSecuritysystemuserusersSecuritysystemroleroleByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSecuritysystemuserusersSecuritysystemroleroleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSecuritysystemuserusersSecuritysystemroleroleCreated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnAfterSecuritysystemuserusersSecuritysystemroleroleCreated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> CreateSecuritysystemuserusersSecuritysystemrolerole(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole securitysystemuseruserssecuritysystemrolerole)
        {
            OnSecuritysystemuserusersSecuritysystemroleroleCreated(securitysystemuseruserssecuritysystemrolerole);

            var existingItem = Context.SecuritysystemuserusersSecuritysystemroleroles
                              .Where(i => i.OID == securitysystemuseruserssecuritysystemrolerole.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SecuritysystemuserusersSecuritysystemroleroles.Add(securitysystemuseruserssecuritysystemrolerole);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(securitysystemuseruserssecuritysystemrolerole).State = EntityState.Detached;
                throw;
            }

            OnAfterSecuritysystemuserusersSecuritysystemroleroleCreated(securitysystemuseruserssecuritysystemrolerole);

            return securitysystemuseruserssecuritysystemrolerole;
        }

        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> CancelSecuritysystemuserusersSecuritysystemroleroleChanges(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSecuritysystemuserusersSecuritysystemroleroleUpdated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnAfterSecuritysystemuserusersSecuritysystemroleroleUpdated(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> UpdateSecuritysystemuserusersSecuritysystemrolerole(Guid oid, SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole securitysystemuseruserssecuritysystemrolerole)
        {
            OnSecuritysystemuserusersSecuritysystemroleroleUpdated(securitysystemuseruserssecuritysystemrolerole);

            var itemToUpdate = Context.SecuritysystemuserusersSecuritysystemroleroles
                              .Where(i => i.OID == securitysystemuseruserssecuritysystemrolerole.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(securitysystemuseruserssecuritysystemrolerole);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSecuritysystemuserusersSecuritysystemroleroleUpdated(securitysystemuseruserssecuritysystemrolerole);

            return securitysystemuseruserssecuritysystemrolerole;
        }

        partial void OnSecuritysystemuserusersSecuritysystemroleroleDeleted(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);
        partial void OnAfterSecuritysystemuserusersSecuritysystemroleroleDeleted(SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole item);

        public async Task<SGPA.Server.Models.CMU.SecuritysystemuserusersSecuritysystemrolerole> DeleteSecuritysystemuserusersSecuritysystemrolerole(Guid oid)
        {
            var itemToDelete = Context.SecuritysystemuserusersSecuritysystemroleroles
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSecuritysystemuserusersSecuritysystemroleroleDeleted(itemToDelete);


            Context.SecuritysystemuserusersSecuritysystemroleroles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSecuritysystemuserusersSecuritysystemroleroleDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSolicitudBajasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSolicitudBajasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSolicitudBajasRead(ref IQueryable<SGPA.Server.Models.CMU.SolicitudBaja> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SolicitudBaja>> GetSolicitudBajas(Query query = null)
        {
            var items = Context.SolicitudBajas.AsQueryable();

            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.ColegiadoDeclaracionJuradum);

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

            OnSolicitudBajasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSolicitudBajaGet(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnGetSolicitudBajaByOid(ref IQueryable<SGPA.Server.Models.CMU.SolicitudBaja> items);


        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> GetSolicitudBajaByOid(int oid)
        {
            var items = Context.SolicitudBajas
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.ColegiadoDeclaracionJuradum);
 
            OnGetSolicitudBajaByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSolicitudBajaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSolicitudBajaCreated(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnAfterSolicitudBajaCreated(SGPA.Server.Models.CMU.SolicitudBaja item);

        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> CreateSolicitudBaja(SGPA.Server.Models.CMU.SolicitudBaja solicitudbaja)
        {
            OnSolicitudBajaCreated(solicitudbaja);

            var existingItem = Context.SolicitudBajas
                              .Where(i => i.OID == solicitudbaja.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SolicitudBajas.Add(solicitudbaja);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(solicitudbaja).State = EntityState.Detached;
                throw;
            }

            OnAfterSolicitudBajaCreated(solicitudbaja);

            return solicitudbaja;
        }

        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> CancelSolicitudBajaChanges(SGPA.Server.Models.CMU.SolicitudBaja item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSolicitudBajaUpdated(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnAfterSolicitudBajaUpdated(SGPA.Server.Models.CMU.SolicitudBaja item);

        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> UpdateSolicitudBaja(int oid, SGPA.Server.Models.CMU.SolicitudBaja solicitudbaja)
        {
            OnSolicitudBajaUpdated(solicitudbaja);

            var itemToUpdate = Context.SolicitudBajas
                              .Where(i => i.OID == solicitudbaja.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(solicitudbaja);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSolicitudBajaUpdated(solicitudbaja);

            return solicitudbaja;
        }

        partial void OnSolicitudBajaDeleted(SGPA.Server.Models.CMU.SolicitudBaja item);
        partial void OnAfterSolicitudBajaDeleted(SGPA.Server.Models.CMU.SolicitudBaja item);

        public async Task<SGPA.Server.Models.CMU.SolicitudBaja> DeleteSolicitudBaja(int oid)
        {
            var itemToDelete = Context.SolicitudBajas
                              .Where(i => i.OID == oid)
                              .Include(i => i.SolicitudBajaFileAttachments)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSolicitudBajaDeleted(itemToDelete);


            Context.SolicitudBajas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSolicitudBajaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSolicitudBajaFileAttachmentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajafileattachments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajafileattachments/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSolicitudBajaFileAttachmentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/solicitudbajafileattachments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/solicitudbajafileattachments/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSolicitudBajaFileAttachmentsRead(ref IQueryable<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment>> GetSolicitudBajaFileAttachments(Query query = null)
        {
            var items = Context.SolicitudBajaFileAttachments.AsQueryable();

            items = items.Include(i => i.FileDatum);
            items = items.Include(i => i.SolicitudBaja);

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

            OnSolicitudBajaFileAttachmentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSolicitudBajaFileAttachmentGet(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnGetSolicitudBajaFileAttachmentByGuid(ref IQueryable<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> items);


        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> GetSolicitudBajaFileAttachmentByGuid(Guid guid)
        {
            var items = Context.SolicitudBajaFileAttachments
                              .AsNoTracking()
                              .Where(i => i.Guid == guid);

            items = items.Include(i => i.FileDatum);
            items = items.Include(i => i.SolicitudBaja);
 
            OnGetSolicitudBajaFileAttachmentByGuid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSolicitudBajaFileAttachmentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSolicitudBajaFileAttachmentCreated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnAfterSolicitudBajaFileAttachmentCreated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);

        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> CreateSolicitudBajaFileAttachment(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment solicitudbajafileattachment)
        {
            OnSolicitudBajaFileAttachmentCreated(solicitudbajafileattachment);

            var existingItem = Context.SolicitudBajaFileAttachments
                              .Where(i => i.Guid == solicitudbajafileattachment.Guid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.SolicitudBajaFileAttachments.Add(solicitudbajafileattachment);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(solicitudbajafileattachment).State = EntityState.Detached;
                throw;
            }

            OnAfterSolicitudBajaFileAttachmentCreated(solicitudbajafileattachment);

            return solicitudbajafileattachment;
        }

        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> CancelSolicitudBajaFileAttachmentChanges(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSolicitudBajaFileAttachmentUpdated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnAfterSolicitudBajaFileAttachmentUpdated(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);

        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> UpdateSolicitudBajaFileAttachment(Guid guid, SGPA.Server.Models.CMU.SolicitudBajaFileAttachment solicitudbajafileattachment)
        {
            OnSolicitudBajaFileAttachmentUpdated(solicitudbajafileattachment);

            var itemToUpdate = Context.SolicitudBajaFileAttachments
                              .Where(i => i.Guid == solicitudbajafileattachment.Guid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(solicitudbajafileattachment);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSolicitudBajaFileAttachmentUpdated(solicitudbajafileattachment);

            return solicitudbajafileattachment;
        }

        partial void OnSolicitudBajaFileAttachmentDeleted(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);
        partial void OnAfterSolicitudBajaFileAttachmentDeleted(SGPA.Server.Models.CMU.SolicitudBajaFileAttachment item);

        public async Task<SGPA.Server.Models.CMU.SolicitudBajaFileAttachment> DeleteSolicitudBajaFileAttachment(Guid guid)
        {
            var itemToDelete = Context.SolicitudBajaFileAttachments
                              .Where(i => i.Guid == guid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSolicitudBajaFileAttachmentDeleted(itemToDelete);


            Context.SolicitudBajaFileAttachments.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSolicitudBajaFileAttachmentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTmpCarneEntregadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneentregados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneentregados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTmpCarneEntregadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneentregados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneentregados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTmpCarneEntregadosRead(ref IQueryable<SGPA.Server.Models.CMU.TmpCarneEntregado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TmpCarneEntregado>> GetTmpCarneEntregados(Query query = null)
        {
            var items = Context.TmpCarneEntregados.AsQueryable();


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

            OnTmpCarneEntregadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTmpCarneEntregadoGet(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnGetTmpCarneEntregadoByDocumento(ref IQueryable<SGPA.Server.Models.CMU.TmpCarneEntregado> items);


        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> GetTmpCarneEntregadoByDocumento(int documento)
        {
            var items = Context.TmpCarneEntregados
                              .AsNoTracking()
                              .Where(i => i.Documento == documento);

 
            OnGetTmpCarneEntregadoByDocumento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTmpCarneEntregadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTmpCarneEntregadoCreated(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnAfterTmpCarneEntregadoCreated(SGPA.Server.Models.CMU.TmpCarneEntregado item);

        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> CreateTmpCarneEntregado(SGPA.Server.Models.CMU.TmpCarneEntregado tmpcarneentregado)
        {
            OnTmpCarneEntregadoCreated(tmpcarneentregado);

            var existingItem = Context.TmpCarneEntregados
                              .Where(i => i.Documento == tmpcarneentregado.Documento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TmpCarneEntregados.Add(tmpcarneentregado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tmpcarneentregado).State = EntityState.Detached;
                throw;
            }

            OnAfterTmpCarneEntregadoCreated(tmpcarneentregado);

            return tmpcarneentregado;
        }

        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> CancelTmpCarneEntregadoChanges(SGPA.Server.Models.CMU.TmpCarneEntregado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTmpCarneEntregadoUpdated(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnAfterTmpCarneEntregadoUpdated(SGPA.Server.Models.CMU.TmpCarneEntregado item);

        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> UpdateTmpCarneEntregado(int documento, SGPA.Server.Models.CMU.TmpCarneEntregado tmpcarneentregado)
        {
            OnTmpCarneEntregadoUpdated(tmpcarneentregado);

            var itemToUpdate = Context.TmpCarneEntregados
                              .Where(i => i.Documento == tmpcarneentregado.Documento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tmpcarneentregado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTmpCarneEntregadoUpdated(tmpcarneentregado);

            return tmpcarneentregado;
        }

        partial void OnTmpCarneEntregadoDeleted(SGPA.Server.Models.CMU.TmpCarneEntregado item);
        partial void OnAfterTmpCarneEntregadoDeleted(SGPA.Server.Models.CMU.TmpCarneEntregado item);

        public async Task<SGPA.Server.Models.CMU.TmpCarneEntregado> DeleteTmpCarneEntregado(int documento)
        {
            var itemToDelete = Context.TmpCarneEntregados
                              .Where(i => i.Documento == documento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTmpCarneEntregadoDeleted(itemToDelete);


            Context.TmpCarneEntregados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTmpCarneEntregadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTmpCarneRetirarsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneretirars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneretirars/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTmpCarneRetirarsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpcarneretirars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpcarneretirars/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTmpCarneRetirarsRead(ref IQueryable<SGPA.Server.Models.CMU.TmpCarneRetirar> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TmpCarneRetirar>> GetTmpCarneRetirars(Query query = null)
        {
            var items = Context.TmpCarneRetirars.AsQueryable();


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

            OnTmpCarneRetirarsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTmpCarneRetirarGet(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnGetTmpCarneRetirarByDocumento(ref IQueryable<SGPA.Server.Models.CMU.TmpCarneRetirar> items);


        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> GetTmpCarneRetirarByDocumento(int documento)
        {
            var items = Context.TmpCarneRetirars
                              .AsNoTracking()
                              .Where(i => i.Documento == documento);

 
            OnGetTmpCarneRetirarByDocumento(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTmpCarneRetirarGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTmpCarneRetirarCreated(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnAfterTmpCarneRetirarCreated(SGPA.Server.Models.CMU.TmpCarneRetirar item);

        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> CreateTmpCarneRetirar(SGPA.Server.Models.CMU.TmpCarneRetirar tmpcarneretirar)
        {
            OnTmpCarneRetirarCreated(tmpcarneretirar);

            var existingItem = Context.TmpCarneRetirars
                              .Where(i => i.Documento == tmpcarneretirar.Documento)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TmpCarneRetirars.Add(tmpcarneretirar);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tmpcarneretirar).State = EntityState.Detached;
                throw;
            }

            OnAfterTmpCarneRetirarCreated(tmpcarneretirar);

            return tmpcarneretirar;
        }

        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> CancelTmpCarneRetirarChanges(SGPA.Server.Models.CMU.TmpCarneRetirar item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTmpCarneRetirarUpdated(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnAfterTmpCarneRetirarUpdated(SGPA.Server.Models.CMU.TmpCarneRetirar item);

        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> UpdateTmpCarneRetirar(int documento, SGPA.Server.Models.CMU.TmpCarneRetirar tmpcarneretirar)
        {
            OnTmpCarneRetirarUpdated(tmpcarneretirar);

            var itemToUpdate = Context.TmpCarneRetirars
                              .Where(i => i.Documento == tmpcarneretirar.Documento)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tmpcarneretirar);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTmpCarneRetirarUpdated(tmpcarneretirar);

            return tmpcarneretirar;
        }

        partial void OnTmpCarneRetirarDeleted(SGPA.Server.Models.CMU.TmpCarneRetirar item);
        partial void OnAfterTmpCarneRetirarDeleted(SGPA.Server.Models.CMU.TmpCarneRetirar item);

        public async Task<SGPA.Server.Models.CMU.TmpCarneRetirar> DeleteTmpCarneRetirar(int documento)
        {
            var itemToDelete = Context.TmpCarneRetirars
                              .Where(i => i.Documento == documento)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTmpCarneRetirarDeleted(itemToDelete);


            Context.TmpCarneRetirars.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTmpCarneRetirarDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTmpFechasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpfechas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpfechas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTmpFechasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpfechas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpfechas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTmpFechasRead(ref IQueryable<SGPA.Server.Models.CMU.TmpFecha> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TmpFecha>> GetTmpFechas(Query query = null)
        {
            var items = Context.TmpFechas.AsQueryable();


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

            OnTmpFechasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTmpFechaGet(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnGetTmpFechaByFecha(ref IQueryable<SGPA.Server.Models.CMU.TmpFecha> items);


        public async Task<SGPA.Server.Models.CMU.TmpFecha> GetTmpFechaByFecha(DateTime fecha)
        {
            var items = Context.TmpFechas
                              .AsNoTracking()
                              .Where(i => i.Fecha == fecha);

 
            OnGetTmpFechaByFecha(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTmpFechaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTmpFechaCreated(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnAfterTmpFechaCreated(SGPA.Server.Models.CMU.TmpFecha item);

        public async Task<SGPA.Server.Models.CMU.TmpFecha> CreateTmpFecha(SGPA.Server.Models.CMU.TmpFecha tmpfecha)
        {
            OnTmpFechaCreated(tmpfecha);

            var existingItem = Context.TmpFechas
                              .Where(i => i.Fecha == tmpfecha.Fecha)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TmpFechas.Add(tmpfecha);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tmpfecha).State = EntityState.Detached;
                throw;
            }

            OnAfterTmpFechaCreated(tmpfecha);

            return tmpfecha;
        }

        public async Task<SGPA.Server.Models.CMU.TmpFecha> CancelTmpFechaChanges(SGPA.Server.Models.CMU.TmpFecha item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTmpFechaUpdated(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnAfterTmpFechaUpdated(SGPA.Server.Models.CMU.TmpFecha item);

        public async Task<SGPA.Server.Models.CMU.TmpFecha> UpdateTmpFecha(DateTime fecha, SGPA.Server.Models.CMU.TmpFecha tmpfecha)
        {
            OnTmpFechaUpdated(tmpfecha);

            var itemToUpdate = Context.TmpFechas
                              .Where(i => i.Fecha == tmpfecha.Fecha)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tmpfecha);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTmpFechaUpdated(tmpfecha);

            return tmpfecha;
        }

        partial void OnTmpFechaDeleted(SGPA.Server.Models.CMU.TmpFecha item);
        partial void OnAfterTmpFechaDeleted(SGPA.Server.Models.CMU.TmpFecha item);

        public async Task<SGPA.Server.Models.CMU.TmpFecha> DeleteTmpFecha(DateTime fecha)
        {
            var itemToDelete = Context.TmpFechas
                              .Where(i => i.Fecha == fecha)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTmpFechaDeleted(itemToDelete);


            Context.TmpFechas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTmpFechaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTmpMesesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpmeses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpmeses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTmpMesesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tmpmeses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tmpmeses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTmpMesesRead(ref IQueryable<SGPA.Server.Models.CMU.TmpMese> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TmpMese>> GetTmpMeses(Query query = null)
        {
            var items = Context.TmpMeses.AsQueryable();


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

            OnTmpMesesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTmpMeseGet(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnGetTmpMeseByMesAndAño(ref IQueryable<SGPA.Server.Models.CMU.TmpMese> items);


        public async Task<SGPA.Server.Models.CMU.TmpMese> GetTmpMeseByMesAndAño(int mes, int año)
        {
            var items = Context.TmpMeses
                              .AsNoTracking()
                              .Where(i => i.Mes == mes && i.Año == año);

 
            OnGetTmpMeseByMesAndAño(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTmpMeseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTmpMeseCreated(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnAfterTmpMeseCreated(SGPA.Server.Models.CMU.TmpMese item);

        public async Task<SGPA.Server.Models.CMU.TmpMese> CreateTmpMese(SGPA.Server.Models.CMU.TmpMese tmpmese)
        {
            OnTmpMeseCreated(tmpmese);

            var existingItem = Context.TmpMeses
                              .Where(i => i.Mes == tmpmese.Mes && i.Año == tmpmese.Año)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TmpMeses.Add(tmpmese);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tmpmese).State = EntityState.Detached;
                throw;
            }

            OnAfterTmpMeseCreated(tmpmese);

            return tmpmese;
        }

        public async Task<SGPA.Server.Models.CMU.TmpMese> CancelTmpMeseChanges(SGPA.Server.Models.CMU.TmpMese item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTmpMeseUpdated(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnAfterTmpMeseUpdated(SGPA.Server.Models.CMU.TmpMese item);

        public async Task<SGPA.Server.Models.CMU.TmpMese> UpdateTmpMese(int mes, int año, SGPA.Server.Models.CMU.TmpMese tmpmese)
        {
            OnTmpMeseUpdated(tmpmese);

            var itemToUpdate = Context.TmpMeses
                              .Where(i => i.Mes == tmpmese.Mes && i.Año == tmpmese.Año)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tmpmese);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTmpMeseUpdated(tmpmese);

            return tmpmese;
        }

        partial void OnTmpMeseDeleted(SGPA.Server.Models.CMU.TmpMese item);
        partial void OnAfterTmpMeseDeleted(SGPA.Server.Models.CMU.TmpMese item);

        public async Task<SGPA.Server.Models.CMU.TmpMese> DeleteTmpMese(int mes, int año)
        {
            var itemToDelete = Context.TmpMeses
                              .Where(i => i.Mes == mes && i.Año == año)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTmpMeseDeleted(itemToDelete);


            Context.TmpMeses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTmpMeseDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteInfoadjuntabasesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntabases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntabases/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteInfoadjuntabasesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntabases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntabases/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteInfoadjuntabasesRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase>> GetTramiteInfoadjuntabases(Query query = null)
        {
            var items = Context.TramiteInfoadjuntabases.AsQueryable();

            items = items.Include(i => i.TramiteCarne);

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

            OnTramiteInfoadjuntabasesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteInfoadjuntabaseGet(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnGetTramiteInfoadjuntabaseByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> items);


        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> GetTramiteInfoadjuntabaseByOid(int oid)
        {
            var items = Context.TramiteInfoadjuntabases
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.TramiteCarne);
 
            OnGetTramiteInfoadjuntabaseByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteInfoadjuntabaseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteInfoadjuntabaseCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnAfterTramiteInfoadjuntabaseCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> CreateTramiteInfoadjuntabase(SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteinfoadjuntabase)
        {
            OnTramiteInfoadjuntabaseCreated(tramiteinfoadjuntabase);

            var existingItem = Context.TramiteInfoadjuntabases
                              .Where(i => i.OID == tramiteinfoadjuntabase.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteInfoadjuntabases.Add(tramiteinfoadjuntabase);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramiteinfoadjuntabase).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteInfoadjuntabaseCreated(tramiteinfoadjuntabase);

            return tramiteinfoadjuntabase;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> CancelTramiteInfoadjuntabaseChanges(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteInfoadjuntabaseUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnAfterTramiteInfoadjuntabaseUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> UpdateTramiteInfoadjuntabase(int oid, SGPA.Server.Models.CMU.TramiteInfoadjuntabase tramiteinfoadjuntabase)
        {
            OnTramiteInfoadjuntabaseUpdated(tramiteinfoadjuntabase);

            var itemToUpdate = Context.TramiteInfoadjuntabases
                              .Where(i => i.OID == tramiteinfoadjuntabase.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramiteinfoadjuntabase);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteInfoadjuntabaseUpdated(tramiteinfoadjuntabase);

            return tramiteinfoadjuntabase;
        }

        partial void OnTramiteInfoadjuntabaseDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);
        partial void OnAfterTramiteInfoadjuntabaseDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntabase item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntabase> DeleteTramiteInfoadjuntabase(int oid)
        {
            var itemToDelete = Context.TramiteInfoadjuntabases
                              .Where(i => i.OID == oid)
                              .Include(i => i.TramiteInfoadjuntacedulas)
                              .Include(i => i.TramiteInfoadjuntafotocarnes)
                              .Include(i => i.TramiteInfoadjuntatitulos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteInfoadjuntabaseDeleted(itemToDelete);


            Context.TramiteInfoadjuntabases.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteInfoadjuntabaseDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteInfoadjuntacedulasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntacedulas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntacedulas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteInfoadjuntacedulasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntacedulas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntacedulas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteInfoadjuntacedulasRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula>> GetTramiteInfoadjuntacedulas(Query query = null)
        {
            var items = Context.TramiteInfoadjuntacedulas.AsQueryable();


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

            OnTramiteInfoadjuntacedulasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteInfoadjuntacedulaGet(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnGetTramiteInfoadjuntacedulaByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> items);


        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> GetTramiteInfoadjuntacedulaByOid(int oid)
        {
            var items = Context.TramiteInfoadjuntacedulas
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

 
            OnGetTramiteInfoadjuntacedulaByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteInfoadjuntacedulaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteInfoadjuntacedulaCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnAfterTramiteInfoadjuntacedulaCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> CreateTramiteInfoadjuntacedula(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula tramiteinfoadjuntacedula)
        {
            OnTramiteInfoadjuntacedulaCreated(tramiteinfoadjuntacedula);

            var existingItem = Context.TramiteInfoadjuntacedulas
                              .Where(i => i.OID == tramiteinfoadjuntacedula.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteInfoadjuntacedulas.Add(tramiteinfoadjuntacedula);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramiteinfoadjuntacedula).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteInfoadjuntacedulaCreated(tramiteinfoadjuntacedula);

            return tramiteinfoadjuntacedula;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> CancelTramiteInfoadjuntacedulaChanges(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteInfoadjuntacedulaUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnAfterTramiteInfoadjuntacedulaUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> UpdateTramiteInfoadjuntacedula(int oid, SGPA.Server.Models.CMU.TramiteInfoadjuntacedula tramiteinfoadjuntacedula)
        {
            OnTramiteInfoadjuntacedulaUpdated(tramiteinfoadjuntacedula);

            var itemToUpdate = Context.TramiteInfoadjuntacedulas
                              .Where(i => i.OID == tramiteinfoadjuntacedula.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramiteinfoadjuntacedula);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteInfoadjuntacedulaUpdated(tramiteinfoadjuntacedula);

            return tramiteinfoadjuntacedula;
        }

        partial void OnTramiteInfoadjuntacedulaDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);
        partial void OnAfterTramiteInfoadjuntacedulaDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntacedula item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntacedula> DeleteTramiteInfoadjuntacedula(int oid)
        {
            var itemToDelete = Context.TramiteInfoadjuntacedulas
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteInfoadjuntacedulaDeleted(itemToDelete);


            Context.TramiteInfoadjuntacedulas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteInfoadjuntacedulaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteInfoadjuntaespecialidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntaespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntaespecialidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteInfoadjuntaespecialidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntaespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntaespecialidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteInfoadjuntaespecialidadsRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad>> GetTramiteInfoadjuntaespecialidads(Query query = null)
        {
            var items = Context.TramiteInfoadjuntaespecialidads.AsQueryable();

            items = items.Include(i => i.Especialidad1);

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

            OnTramiteInfoadjuntaespecialidadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteInfoadjuntaespecialidadGet(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnGetTramiteInfoadjuntaespecialidadByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> items);


        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> GetTramiteInfoadjuntaespecialidadByOid(int oid)
        {
            var items = Context.TramiteInfoadjuntaespecialidads
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.Especialidad1);
 
            OnGetTramiteInfoadjuntaespecialidadByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteInfoadjuntaespecialidadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteInfoadjuntaespecialidadCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnAfterTramiteInfoadjuntaespecialidadCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> CreateTramiteInfoadjuntaespecialidad(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad tramiteinfoadjuntaespecialidad)
        {
            OnTramiteInfoadjuntaespecialidadCreated(tramiteinfoadjuntaespecialidad);

            var existingItem = Context.TramiteInfoadjuntaespecialidads
                              .Where(i => i.OID == tramiteinfoadjuntaespecialidad.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteInfoadjuntaespecialidads.Add(tramiteinfoadjuntaespecialidad);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramiteinfoadjuntaespecialidad).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteInfoadjuntaespecialidadCreated(tramiteinfoadjuntaespecialidad);

            return tramiteinfoadjuntaespecialidad;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> CancelTramiteInfoadjuntaespecialidadChanges(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteInfoadjuntaespecialidadUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnAfterTramiteInfoadjuntaespecialidadUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> UpdateTramiteInfoadjuntaespecialidad(int oid, SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad tramiteinfoadjuntaespecialidad)
        {
            OnTramiteInfoadjuntaespecialidadUpdated(tramiteinfoadjuntaespecialidad);

            var itemToUpdate = Context.TramiteInfoadjuntaespecialidads
                              .Where(i => i.OID == tramiteinfoadjuntaespecialidad.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramiteinfoadjuntaespecialidad);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteInfoadjuntaespecialidadUpdated(tramiteinfoadjuntaespecialidad);

            return tramiteinfoadjuntaespecialidad;
        }

        partial void OnTramiteInfoadjuntaespecialidadDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);
        partial void OnAfterTramiteInfoadjuntaespecialidadDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntaespecialidad> DeleteTramiteInfoadjuntaespecialidad(int oid)
        {
            var itemToDelete = Context.TramiteInfoadjuntaespecialidads
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteInfoadjuntaespecialidadDeleted(itemToDelete);


            Context.TramiteInfoadjuntaespecialidads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteInfoadjuntaespecialidadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteInfoadjuntafotocarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntafotocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntafotocarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteInfoadjuntafotocarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntafotocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntafotocarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteInfoadjuntafotocarnesRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne>> GetTramiteInfoadjuntafotocarnes(Query query = null)
        {
            var items = Context.TramiteInfoadjuntafotocarnes.AsQueryable();


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

            OnTramiteInfoadjuntafotocarnesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteInfoadjuntafotocarneGet(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnGetTramiteInfoadjuntafotocarneByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> items);


        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> GetTramiteInfoadjuntafotocarneByOid(int oid)
        {
            var items = Context.TramiteInfoadjuntafotocarnes
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

 
            OnGetTramiteInfoadjuntafotocarneByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteInfoadjuntafotocarneGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteInfoadjuntafotocarneCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnAfterTramiteInfoadjuntafotocarneCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> CreateTramiteInfoadjuntafotocarne(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne tramiteinfoadjuntafotocarne)
        {
            OnTramiteInfoadjuntafotocarneCreated(tramiteinfoadjuntafotocarne);

            var existingItem = Context.TramiteInfoadjuntafotocarnes
                              .Where(i => i.OID == tramiteinfoadjuntafotocarne.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteInfoadjuntafotocarnes.Add(tramiteinfoadjuntafotocarne);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramiteinfoadjuntafotocarne).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteInfoadjuntafotocarneCreated(tramiteinfoadjuntafotocarne);

            return tramiteinfoadjuntafotocarne;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> CancelTramiteInfoadjuntafotocarneChanges(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteInfoadjuntafotocarneUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnAfterTramiteInfoadjuntafotocarneUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> UpdateTramiteInfoadjuntafotocarne(int oid, SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne tramiteinfoadjuntafotocarne)
        {
            OnTramiteInfoadjuntafotocarneUpdated(tramiteinfoadjuntafotocarne);

            var itemToUpdate = Context.TramiteInfoadjuntafotocarnes
                              .Where(i => i.OID == tramiteinfoadjuntafotocarne.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramiteinfoadjuntafotocarne);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteInfoadjuntafotocarneUpdated(tramiteinfoadjuntafotocarne);

            return tramiteinfoadjuntafotocarne;
        }

        partial void OnTramiteInfoadjuntafotocarneDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);
        partial void OnAfterTramiteInfoadjuntafotocarneDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntafotocarne> DeleteTramiteInfoadjuntafotocarne(int oid)
        {
            var itemToDelete = Context.TramiteInfoadjuntafotocarnes
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteInfoadjuntafotocarneDeleted(itemToDelete);


            Context.TramiteInfoadjuntafotocarnes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteInfoadjuntafotocarneDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteInfoadjuntatitulosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntatitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntatitulos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteInfoadjuntatitulosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramiteinfoadjuntatitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramiteinfoadjuntatitulos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteInfoadjuntatitulosRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo>> GetTramiteInfoadjuntatitulos(Query query = null)
        {
            var items = Context.TramiteInfoadjuntatitulos.AsQueryable();

            items = items.Include(i => i.UniversidadTituloGrado);

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

            OnTramiteInfoadjuntatitulosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteInfoadjuntatituloGet(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnGetTramiteInfoadjuntatituloByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> items);


        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> GetTramiteInfoadjuntatituloByOid(int oid)
        {
            var items = Context.TramiteInfoadjuntatitulos
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.UniversidadTituloGrado);
 
            OnGetTramiteInfoadjuntatituloByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteInfoadjuntatituloGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteInfoadjuntatituloCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnAfterTramiteInfoadjuntatituloCreated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> CreateTramiteInfoadjuntatitulo(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo tramiteinfoadjuntatitulo)
        {
            OnTramiteInfoadjuntatituloCreated(tramiteinfoadjuntatitulo);

            var existingItem = Context.TramiteInfoadjuntatitulos
                              .Where(i => i.OID == tramiteinfoadjuntatitulo.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteInfoadjuntatitulos.Add(tramiteinfoadjuntatitulo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramiteinfoadjuntatitulo).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteInfoadjuntatituloCreated(tramiteinfoadjuntatitulo);

            return tramiteinfoadjuntatitulo;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> CancelTramiteInfoadjuntatituloChanges(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteInfoadjuntatituloUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnAfterTramiteInfoadjuntatituloUpdated(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> UpdateTramiteInfoadjuntatitulo(int oid, SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo tramiteinfoadjuntatitulo)
        {
            OnTramiteInfoadjuntatituloUpdated(tramiteinfoadjuntatitulo);

            var itemToUpdate = Context.TramiteInfoadjuntatitulos
                              .Where(i => i.OID == tramiteinfoadjuntatitulo.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramiteinfoadjuntatitulo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteInfoadjuntatituloUpdated(tramiteinfoadjuntatitulo);

            return tramiteinfoadjuntatitulo;
        }

        partial void OnTramiteInfoadjuntatituloDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);
        partial void OnAfterTramiteInfoadjuntatituloDeleted(SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo item);

        public async Task<SGPA.Server.Models.CMU.TramiteInfoadjuntatitulo> DeleteTramiteInfoadjuntatitulo(int oid)
        {
            var itemToDelete = Context.TramiteInfoadjuntatitulos
                              .Where(i => i.OID == oid)
                              .Include(i => i.TramiteInfoadjuntaespecialidads)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteInfoadjuntatituloDeleted(itemToDelete);


            Context.TramiteInfoadjuntatitulos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteInfoadjuntatituloDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteCarnesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarnes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteCarnesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarnes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteCarnesRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarne> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteCarne>> GetTramiteCarnes(Query query = null)
        {
            var items = Context.TramiteCarnes.AsQueryable();

            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.LugarRetiroCarne);

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

            OnTramiteCarnesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteCarneGet(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnGetTramiteCarneByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarne> items);


        public async Task<SGPA.Server.Models.CMU.TramiteCarne> GetTramiteCarneByOid(int oid)
        {
            var items = Context.TramiteCarnes
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.Colegiado1);
            items = items.Include(i => i.LugarRetiroCarne);
 
            OnGetTramiteCarneByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteCarneGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteCarneCreated(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnAfterTramiteCarneCreated(SGPA.Server.Models.CMU.TramiteCarne item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarne> CreateTramiteCarne(SGPA.Server.Models.CMU.TramiteCarne tramitecarne)
        {
            OnTramiteCarneCreated(tramitecarne);

            var existingItem = Context.TramiteCarnes
                              .Where(i => i.OID == tramitecarne.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteCarnes.Add(tramitecarne);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramitecarne).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteCarneCreated(tramitecarne);

            return tramitecarne;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteCarne> CancelTramiteCarneChanges(SGPA.Server.Models.CMU.TramiteCarne item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteCarneUpdated(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnAfterTramiteCarneUpdated(SGPA.Server.Models.CMU.TramiteCarne item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarne> UpdateTramiteCarne(int oid, SGPA.Server.Models.CMU.TramiteCarne tramitecarne)
        {
            OnTramiteCarneUpdated(tramitecarne);

            var itemToUpdate = Context.TramiteCarnes
                              .Where(i => i.OID == tramitecarne.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramitecarne);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteCarneUpdated(tramitecarne);

            return tramitecarne;
        }

        partial void OnTramiteCarneDeleted(SGPA.Server.Models.CMU.TramiteCarne item);
        partial void OnAfterTramiteCarneDeleted(SGPA.Server.Models.CMU.TramiteCarne item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarne> DeleteTramiteCarne(int oid)
        {
            var itemToDelete = Context.TramiteCarnes
                              .Where(i => i.OID == oid)
                              .Include(i => i.TramiteInfoadjuntabases)
                              .Include(i => i.TramiteCarneEstados)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteCarneDeleted(itemToDelete);


            Context.TramiteCarnes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteCarneDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteCarneEstadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteCarneEstadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteCarneEstadosRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstado>> GetTramiteCarneEstados(Query query = null)
        {
            var items = Context.TramiteCarneEstados.AsQueryable();

            items = items.Include(i => i.TramiteCarneEstadoCodigo);
            items = items.Include(i => i.TramiteCarne);

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

            OnTramiteCarneEstadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteCarneEstadoGet(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnGetTramiteCarneEstadoByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstado> items);


        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> GetTramiteCarneEstadoByOid(int oid)
        {
            var items = Context.TramiteCarneEstados
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.TramiteCarneEstadoCodigo);
            items = items.Include(i => i.TramiteCarne);
 
            OnGetTramiteCarneEstadoByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteCarneEstadoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteCarneEstadoCreated(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnAfterTramiteCarneEstadoCreated(SGPA.Server.Models.CMU.TramiteCarneEstado item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> CreateTramiteCarneEstado(SGPA.Server.Models.CMU.TramiteCarneEstado tramitecarneestado)
        {
            OnTramiteCarneEstadoCreated(tramitecarneestado);

            var existingItem = Context.TramiteCarneEstados
                              .Where(i => i.OID == tramitecarneestado.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteCarneEstados.Add(tramitecarneestado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramitecarneestado).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteCarneEstadoCreated(tramitecarneestado);

            return tramitecarneestado;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> CancelTramiteCarneEstadoChanges(SGPA.Server.Models.CMU.TramiteCarneEstado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteCarneEstadoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnAfterTramiteCarneEstadoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstado item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> UpdateTramiteCarneEstado(int oid, SGPA.Server.Models.CMU.TramiteCarneEstado tramitecarneestado)
        {
            OnTramiteCarneEstadoUpdated(tramitecarneestado);

            var itemToUpdate = Context.TramiteCarneEstados
                              .Where(i => i.OID == tramitecarneestado.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramitecarneestado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteCarneEstadoUpdated(tramitecarneestado);

            return tramitecarneestado;
        }

        partial void OnTramiteCarneEstadoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstado item);
        partial void OnAfterTramiteCarneEstadoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstado item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstado> DeleteTramiteCarneEstado(int oid)
        {
            var itemToDelete = Context.TramiteCarneEstados
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteCarneEstadoDeleted(itemToDelete);


            Context.TramiteCarneEstados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteCarneEstadoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteCarneEstadoCodigosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadocodigos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadocodigos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteCarneEstadoCodigosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadocodigos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadocodigos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteCarneEstadoCodigosRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo>> GetTramiteCarneEstadoCodigos(Query query = null)
        {
            var items = Context.TramiteCarneEstadoCodigos.AsQueryable();


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

            OnTramiteCarneEstadoCodigosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteCarneEstadoCodigoGet(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnGetTramiteCarneEstadoCodigoById(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> items);


        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> GetTramiteCarneEstadoCodigoById(int id)
        {
            var items = Context.TramiteCarneEstadoCodigos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetTramiteCarneEstadoCodigoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteCarneEstadoCodigoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteCarneEstadoCodigoCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnAfterTramiteCarneEstadoCodigoCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> CreateTramiteCarneEstadoCodigo(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramitecarneestadocodigo)
        {
            OnTramiteCarneEstadoCodigoCreated(tramitecarneestadocodigo);

            var existingItem = Context.TramiteCarneEstadoCodigos
                              .Where(i => i.Id == tramitecarneestadocodigo.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteCarneEstadoCodigos.Add(tramitecarneestadocodigo);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramitecarneestadocodigo).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteCarneEstadoCodigoCreated(tramitecarneestadocodigo);

            return tramitecarneestadocodigo;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> CancelTramiteCarneEstadoCodigoChanges(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteCarneEstadoCodigoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnAfterTramiteCarneEstadoCodigoUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> UpdateTramiteCarneEstadoCodigo(int id, SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo tramitecarneestadocodigo)
        {
            OnTramiteCarneEstadoCodigoUpdated(tramitecarneestadocodigo);

            var itemToUpdate = Context.TramiteCarneEstadoCodigos
                              .Where(i => i.Id == tramitecarneestadocodigo.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramitecarneestadocodigo);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteCarneEstadoCodigoUpdated(tramitecarneestadocodigo);

            return tramitecarneestadocodigo;
        }

        partial void OnTramiteCarneEstadoCodigoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);
        partial void OnAfterTramiteCarneEstadoCodigoDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoCodigo> DeleteTramiteCarneEstadoCodigo(int id)
        {
            var itemToDelete = Context.TramiteCarneEstadoCodigos
                              .Where(i => i.Id == id)
                              .Include(i => i.TramiteCarneEstados)
                              .Include(i => i.TramiteCarneEstadoWorkFlows)
                              .Include(i => i.TramiteCarneEstadoWorkFlows1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteCarneEstadoCodigoDeleted(itemToDelete);


            Context.TramiteCarneEstadoCodigos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteCarneEstadoCodigoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTramiteCarneEstadoWorkFlowsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadoworkflows/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadoworkflows/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTramiteCarneEstadoWorkFlowsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/tramitecarneestadoworkflows/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/tramitecarneestadoworkflows/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTramiteCarneEstadoWorkFlowsRead(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow>> GetTramiteCarneEstadoWorkFlows(Query query = null)
        {
            var items = Context.TramiteCarneEstadoWorkFlows.AsQueryable();

            items = items.Include(i => i.TramiteCarneEstadoCodigo);
            items = items.Include(i => i.TramiteCarneEstadoCodigo1);

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

            OnTramiteCarneEstadoWorkFlowsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTramiteCarneEstadoWorkFlowGet(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnGetTramiteCarneEstadoWorkFlowByOid(ref IQueryable<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> items);


        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> GetTramiteCarneEstadoWorkFlowByOid(int oid)
        {
            var items = Context.TramiteCarneEstadoWorkFlows
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

            items = items.Include(i => i.TramiteCarneEstadoCodigo);
            items = items.Include(i => i.TramiteCarneEstadoCodigo1);
 
            OnGetTramiteCarneEstadoWorkFlowByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTramiteCarneEstadoWorkFlowGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTramiteCarneEstadoWorkFlowCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnAfterTramiteCarneEstadoWorkFlowCreated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> CreateTramiteCarneEstadoWorkFlow(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow tramitecarneestadoworkflow)
        {
            OnTramiteCarneEstadoWorkFlowCreated(tramitecarneestadoworkflow);

            var existingItem = Context.TramiteCarneEstadoWorkFlows
                              .Where(i => i.OID == tramitecarneestadoworkflow.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.TramiteCarneEstadoWorkFlows.Add(tramitecarneestadoworkflow);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tramitecarneestadoworkflow).State = EntityState.Detached;
                throw;
            }

            OnAfterTramiteCarneEstadoWorkFlowCreated(tramitecarneestadoworkflow);

            return tramitecarneestadoworkflow;
        }

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> CancelTramiteCarneEstadoWorkFlowChanges(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTramiteCarneEstadoWorkFlowUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnAfterTramiteCarneEstadoWorkFlowUpdated(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> UpdateTramiteCarneEstadoWorkFlow(int oid, SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow tramitecarneestadoworkflow)
        {
            OnTramiteCarneEstadoWorkFlowUpdated(tramitecarneestadoworkflow);

            var itemToUpdate = Context.TramiteCarneEstadoWorkFlows
                              .Where(i => i.OID == tramitecarneestadoworkflow.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tramitecarneestadoworkflow);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTramiteCarneEstadoWorkFlowUpdated(tramitecarneestadoworkflow);

            return tramitecarneestadoworkflow;
        }

        partial void OnTramiteCarneEstadoWorkFlowDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);
        partial void OnAfterTramiteCarneEstadoWorkFlowDeleted(SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow item);

        public async Task<SGPA.Server.Models.CMU.TramiteCarneEstadoWorkFlow> DeleteTramiteCarneEstadoWorkFlow(int oid)
        {
            var itemToDelete = Context.TramiteCarneEstadoWorkFlows
                              .Where(i => i.OID == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTramiteCarneEstadoWorkFlowDeleted(itemToDelete);


            Context.TramiteCarneEstadoWorkFlows.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTramiteCarneEstadoWorkFlowDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUniversidadsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidads/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUniversidadsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidads/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUniversidadsRead(ref IQueryable<SGPA.Server.Models.CMU.Universidad> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Universidad>> GetUniversidads(Query query = null)
        {
            var items = Context.Universidads.AsQueryable();


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

            OnUniversidadsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUniversidadGet(SGPA.Server.Models.CMU.Universidad item);
        partial void OnGetUniversidadById(ref IQueryable<SGPA.Server.Models.CMU.Universidad> items);


        public async Task<SGPA.Server.Models.CMU.Universidad> GetUniversidadById(int id)
        {
            var items = Context.Universidads
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetUniversidadById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUniversidadGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUniversidadCreated(SGPA.Server.Models.CMU.Universidad item);
        partial void OnAfterUniversidadCreated(SGPA.Server.Models.CMU.Universidad item);

        public async Task<SGPA.Server.Models.CMU.Universidad> CreateUniversidad(SGPA.Server.Models.CMU.Universidad universidad)
        {
            OnUniversidadCreated(universidad);

            var existingItem = Context.Universidads
                              .Where(i => i.Id == universidad.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Universidads.Add(universidad);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(universidad).State = EntityState.Detached;
                throw;
            }

            OnAfterUniversidadCreated(universidad);

            return universidad;
        }

        public async Task<SGPA.Server.Models.CMU.Universidad> CancelUniversidadChanges(SGPA.Server.Models.CMU.Universidad item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUniversidadUpdated(SGPA.Server.Models.CMU.Universidad item);
        partial void OnAfterUniversidadUpdated(SGPA.Server.Models.CMU.Universidad item);

        public async Task<SGPA.Server.Models.CMU.Universidad> UpdateUniversidad(int id, SGPA.Server.Models.CMU.Universidad universidad)
        {
            OnUniversidadUpdated(universidad);

            var itemToUpdate = Context.Universidads
                              .Where(i => i.Id == universidad.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(universidad);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUniversidadUpdated(universidad);

            return universidad;
        }

        partial void OnUniversidadDeleted(SGPA.Server.Models.CMU.Universidad item);
        partial void OnAfterUniversidadDeleted(SGPA.Server.Models.CMU.Universidad item);

        public async Task<SGPA.Server.Models.CMU.Universidad> DeleteUniversidad(int id)
        {
            var itemToDelete = Context.Universidads
                              .Where(i => i.Id == id)
                              .Include(i => i.RegistroColegiados)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUniversidadDeleted(itemToDelete);


            Context.Universidads.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUniversidadDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUniversidadTituloGradosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidadtitulogrados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidadtitulogrados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUniversidadTituloGradosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/universidadtitulogrados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/universidadtitulogrados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUniversidadTituloGradosRead(ref IQueryable<SGPA.Server.Models.CMU.UniversidadTituloGrado> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.UniversidadTituloGrado>> GetUniversidadTituloGrados(Query query = null)
        {
            var items = Context.UniversidadTituloGrados.AsQueryable();


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

            OnUniversidadTituloGradosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUniversidadTituloGradoGet(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnGetUniversidadTituloGradoById(ref IQueryable<SGPA.Server.Models.CMU.UniversidadTituloGrado> items);


        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> GetUniversidadTituloGradoById(int id)
        {
            var items = Context.UniversidadTituloGrados
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetUniversidadTituloGradoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUniversidadTituloGradoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUniversidadTituloGradoCreated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnAfterUniversidadTituloGradoCreated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);

        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> CreateUniversidadTituloGrado(SGPA.Server.Models.CMU.UniversidadTituloGrado universidadtitulogrado)
        {
            OnUniversidadTituloGradoCreated(universidadtitulogrado);

            var existingItem = Context.UniversidadTituloGrados
                              .Where(i => i.Id == universidadtitulogrado.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.UniversidadTituloGrados.Add(universidadtitulogrado);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(universidadtitulogrado).State = EntityState.Detached;
                throw;
            }

            OnAfterUniversidadTituloGradoCreated(universidadtitulogrado);

            return universidadtitulogrado;
        }

        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> CancelUniversidadTituloGradoChanges(SGPA.Server.Models.CMU.UniversidadTituloGrado item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUniversidadTituloGradoUpdated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnAfterUniversidadTituloGradoUpdated(SGPA.Server.Models.CMU.UniversidadTituloGrado item);

        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> UpdateUniversidadTituloGrado(int id, SGPA.Server.Models.CMU.UniversidadTituloGrado universidadtitulogrado)
        {
            OnUniversidadTituloGradoUpdated(universidadtitulogrado);

            var itemToUpdate = Context.UniversidadTituloGrados
                              .Where(i => i.Id == universidadtitulogrado.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(universidadtitulogrado);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUniversidadTituloGradoUpdated(universidadtitulogrado);

            return universidadtitulogrado;
        }

        partial void OnUniversidadTituloGradoDeleted(SGPA.Server.Models.CMU.UniversidadTituloGrado item);
        partial void OnAfterUniversidadTituloGradoDeleted(SGPA.Server.Models.CMU.UniversidadTituloGrado item);

        public async Task<SGPA.Server.Models.CMU.UniversidadTituloGrado> DeleteUniversidadTituloGrado(int id)
        {
            var itemToDelete = Context.UniversidadTituloGrados
                              .Where(i => i.Id == id)
                              .Include(i => i.Colegiados)
                              .Include(i => i.RegistroColegiados)
                              .Include(i => i.TramiteInfoadjuntatitulos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUniversidadTituloGradoDeleted(itemToDelete);


            Context.UniversidadTituloGrados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUniversidadTituloGradoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUserResetPasswordRequestsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/userresetpasswordrequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/userresetpasswordrequests/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUserResetPasswordRequestsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/userresetpasswordrequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/userresetpasswordrequests/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUserResetPasswordRequestsRead(ref IQueryable<SGPA.Server.Models.CMU.UserResetPasswordRequest> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.UserResetPasswordRequest>> GetUserResetPasswordRequests(Query query = null)
        {
            var items = Context.UserResetPasswordRequests.AsQueryable();


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

            OnUserResetPasswordRequestsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserResetPasswordRequestGet(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnGetUserResetPasswordRequestById(ref IQueryable<SGPA.Server.Models.CMU.UserResetPasswordRequest> items);


        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> GetUserResetPasswordRequestById(int id)
        {
            var items = Context.UserResetPasswordRequests
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetUserResetPasswordRequestById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUserResetPasswordRequestGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUserResetPasswordRequestCreated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnAfterUserResetPasswordRequestCreated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);

        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> CreateUserResetPasswordRequest(SGPA.Server.Models.CMU.UserResetPasswordRequest userresetpasswordrequest)
        {
            OnUserResetPasswordRequestCreated(userresetpasswordrequest);

            var existingItem = Context.UserResetPasswordRequests
                              .Where(i => i.Id == userresetpasswordrequest.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.UserResetPasswordRequests.Add(userresetpasswordrequest);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(userresetpasswordrequest).State = EntityState.Detached;
                throw;
            }

            OnAfterUserResetPasswordRequestCreated(userresetpasswordrequest);

            return userresetpasswordrequest;
        }

        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> CancelUserResetPasswordRequestChanges(SGPA.Server.Models.CMU.UserResetPasswordRequest item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUserResetPasswordRequestUpdated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnAfterUserResetPasswordRequestUpdated(SGPA.Server.Models.CMU.UserResetPasswordRequest item);

        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> UpdateUserResetPasswordRequest(int id, SGPA.Server.Models.CMU.UserResetPasswordRequest userresetpasswordrequest)
        {
            OnUserResetPasswordRequestUpdated(userresetpasswordrequest);

            var itemToUpdate = Context.UserResetPasswordRequests
                              .Where(i => i.Id == userresetpasswordrequest.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(userresetpasswordrequest);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUserResetPasswordRequestUpdated(userresetpasswordrequest);

            return userresetpasswordrequest;
        }

        partial void OnUserResetPasswordRequestDeleted(SGPA.Server.Models.CMU.UserResetPasswordRequest item);
        partial void OnAfterUserResetPasswordRequestDeleted(SGPA.Server.Models.CMU.UserResetPasswordRequest item);

        public async Task<SGPA.Server.Models.CMU.UserResetPasswordRequest> DeleteUserResetPasswordRequest(int id)
        {
            var itemToDelete = Context.UserResetPasswordRequests
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserResetPasswordRequestDeleted(itemToDelete);


            Context.UserResetPasswordRequests.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserResetPasswordRequestDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsuariosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsuariosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsuariosRead(ref IQueryable<SGPA.Server.Models.CMU.Usuario> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.Usuario>> GetUsuarios(Query query = null)
        {
            var items = Context.Usuarios.AsQueryable();

            items = items.Include(i => i.SecuritySystemUser);

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

            OnUsuariosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsuarioGet(SGPA.Server.Models.CMU.Usuario item);
        partial void OnGetUsuarioByOid(ref IQueryable<SGPA.Server.Models.CMU.Usuario> items);


        public async Task<SGPA.Server.Models.CMU.Usuario> GetUsuarioByOid(Guid oid)
        {
            var items = Context.Usuarios
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.SecuritySystemUser);
 
            OnGetUsuarioByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUsuarioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUsuarioCreated(SGPA.Server.Models.CMU.Usuario item);
        partial void OnAfterUsuarioCreated(SGPA.Server.Models.CMU.Usuario item);

        public async Task<SGPA.Server.Models.CMU.Usuario> CreateUsuario(SGPA.Server.Models.CMU.Usuario usuario)
        {
            OnUsuarioCreated(usuario);

            var existingItem = Context.Usuarios
                              .Where(i => i.Oid == usuario.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Usuarios.Add(usuario);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(usuario).State = EntityState.Detached;
                throw;
            }

            OnAfterUsuarioCreated(usuario);

            return usuario;
        }

        public async Task<SGPA.Server.Models.CMU.Usuario> CancelUsuarioChanges(SGPA.Server.Models.CMU.Usuario item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUsuarioUpdated(SGPA.Server.Models.CMU.Usuario item);
        partial void OnAfterUsuarioUpdated(SGPA.Server.Models.CMU.Usuario item);

        public async Task<SGPA.Server.Models.CMU.Usuario> UpdateUsuario(Guid oid, SGPA.Server.Models.CMU.Usuario usuario)
        {
            OnUsuarioUpdated(usuario);

            var itemToUpdate = Context.Usuarios
                              .Where(i => i.Oid == usuario.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(usuario);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUsuarioUpdated(usuario);

            return usuario;
        }

        partial void OnUsuarioDeleted(SGPA.Server.Models.CMU.Usuario item);
        partial void OnAfterUsuarioDeleted(SGPA.Server.Models.CMU.Usuario item);

        public async Task<SGPA.Server.Models.CMU.Usuario> DeleteUsuario(Guid oid)
        {
            var itemToDelete = Context.Usuarios
                              .Where(i => i.Oid == oid)
                              .Include(i => i.UsuarioAccesos)
                              .Include(i => i.UsuarioInstitucions)
                              .Include(i => i.UsuarioRegionals)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsuarioDeleted(itemToDelete);


            Context.Usuarios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUsuarioDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsuarioAccesosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioaccesos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioaccesos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsuarioAccesosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioaccesos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioaccesos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsuarioAccesosRead(ref IQueryable<SGPA.Server.Models.CMU.UsuarioAcceso> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.UsuarioAcceso>> GetUsuarioAccesos(Query query = null)
        {
            var items = Context.UsuarioAccesos.AsQueryable();

            items = items.Include(i => i.Usuario1);

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

            OnUsuarioAccesosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsuarioAccesoGet(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnGetUsuarioAccesoById(ref IQueryable<SGPA.Server.Models.CMU.UsuarioAcceso> items);


        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> GetUsuarioAccesoById(int id)
        {
            var items = Context.UsuarioAccesos
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Usuario1);
 
            OnGetUsuarioAccesoById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUsuarioAccesoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUsuarioAccesoCreated(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnAfterUsuarioAccesoCreated(SGPA.Server.Models.CMU.UsuarioAcceso item);

        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> CreateUsuarioAcceso(SGPA.Server.Models.CMU.UsuarioAcceso usuarioacceso)
        {
            OnUsuarioAccesoCreated(usuarioacceso);

            var existingItem = Context.UsuarioAccesos
                              .Where(i => i.Id == usuarioacceso.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.UsuarioAccesos.Add(usuarioacceso);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(usuarioacceso).State = EntityState.Detached;
                throw;
            }

            OnAfterUsuarioAccesoCreated(usuarioacceso);

            return usuarioacceso;
        }

        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> CancelUsuarioAccesoChanges(SGPA.Server.Models.CMU.UsuarioAcceso item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUsuarioAccesoUpdated(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnAfterUsuarioAccesoUpdated(SGPA.Server.Models.CMU.UsuarioAcceso item);

        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> UpdateUsuarioAcceso(int id, SGPA.Server.Models.CMU.UsuarioAcceso usuarioacceso)
        {
            OnUsuarioAccesoUpdated(usuarioacceso);

            var itemToUpdate = Context.UsuarioAccesos
                              .Where(i => i.Id == usuarioacceso.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(usuarioacceso);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUsuarioAccesoUpdated(usuarioacceso);

            return usuarioacceso;
        }

        partial void OnUsuarioAccesoDeleted(SGPA.Server.Models.CMU.UsuarioAcceso item);
        partial void OnAfterUsuarioAccesoDeleted(SGPA.Server.Models.CMU.UsuarioAcceso item);

        public async Task<SGPA.Server.Models.CMU.UsuarioAcceso> DeleteUsuarioAcceso(int id)
        {
            var itemToDelete = Context.UsuarioAccesos
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsuarioAccesoDeleted(itemToDelete);


            Context.UsuarioAccesos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUsuarioAccesoDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsuarioInstitucionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioinstitucions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioinstitucions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsuarioInstitucionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioinstitucions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioinstitucions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsuarioInstitucionsRead(ref IQueryable<SGPA.Server.Models.CMU.UsuarioInstitucion> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.UsuarioInstitucion>> GetUsuarioInstitucions(Query query = null)
        {
            var items = Context.UsuarioInstitucions.AsQueryable();

            items = items.Include(i => i.AgenteCobranza);
            items = items.Include(i => i.Usuario);

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

            OnUsuarioInstitucionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsuarioInstitucionGet(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnGetUsuarioInstitucionByOid(ref IQueryable<SGPA.Server.Models.CMU.UsuarioInstitucion> items);


        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> GetUsuarioInstitucionByOid(Guid oid)
        {
            var items = Context.UsuarioInstitucions
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.AgenteCobranza);
            items = items.Include(i => i.Usuario);
 
            OnGetUsuarioInstitucionByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUsuarioInstitucionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUsuarioInstitucionCreated(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnAfterUsuarioInstitucionCreated(SGPA.Server.Models.CMU.UsuarioInstitucion item);

        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> CreateUsuarioInstitucion(SGPA.Server.Models.CMU.UsuarioInstitucion usuarioinstitucion)
        {
            OnUsuarioInstitucionCreated(usuarioinstitucion);

            var existingItem = Context.UsuarioInstitucions
                              .Where(i => i.Oid == usuarioinstitucion.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.UsuarioInstitucions.Add(usuarioinstitucion);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(usuarioinstitucion).State = EntityState.Detached;
                throw;
            }

            OnAfterUsuarioInstitucionCreated(usuarioinstitucion);

            return usuarioinstitucion;
        }

        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> CancelUsuarioInstitucionChanges(SGPA.Server.Models.CMU.UsuarioInstitucion item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUsuarioInstitucionUpdated(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnAfterUsuarioInstitucionUpdated(SGPA.Server.Models.CMU.UsuarioInstitucion item);

        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> UpdateUsuarioInstitucion(Guid oid, SGPA.Server.Models.CMU.UsuarioInstitucion usuarioinstitucion)
        {
            OnUsuarioInstitucionUpdated(usuarioinstitucion);

            var itemToUpdate = Context.UsuarioInstitucions
                              .Where(i => i.Oid == usuarioinstitucion.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(usuarioinstitucion);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUsuarioInstitucionUpdated(usuarioinstitucion);

            return usuarioinstitucion;
        }

        partial void OnUsuarioInstitucionDeleted(SGPA.Server.Models.CMU.UsuarioInstitucion item);
        partial void OnAfterUsuarioInstitucionDeleted(SGPA.Server.Models.CMU.UsuarioInstitucion item);

        public async Task<SGPA.Server.Models.CMU.UsuarioInstitucion> DeleteUsuarioInstitucion(Guid oid)
        {
            var itemToDelete = Context.UsuarioInstitucions
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsuarioInstitucionDeleted(itemToDelete);


            Context.UsuarioInstitucions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUsuarioInstitucionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsuarioRegionalsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioregionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioregionals/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsuarioRegionalsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/usuarioregionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/usuarioregionals/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsuarioRegionalsRead(ref IQueryable<SGPA.Server.Models.CMU.UsuarioRegional> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.UsuarioRegional>> GetUsuarioRegionals(Query query = null)
        {
            var items = Context.UsuarioRegionals.AsQueryable();

            items = items.Include(i => i.Usuario);
            items = items.Include(i => i.Regional1);

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

            OnUsuarioRegionalsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsuarioRegionalGet(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnGetUsuarioRegionalByOid(ref IQueryable<SGPA.Server.Models.CMU.UsuarioRegional> items);


        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> GetUsuarioRegionalByOid(Guid oid)
        {
            var items = Context.UsuarioRegionals
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.Usuario);
            items = items.Include(i => i.Regional1);
 
            OnGetUsuarioRegionalByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUsuarioRegionalGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUsuarioRegionalCreated(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnAfterUsuarioRegionalCreated(SGPA.Server.Models.CMU.UsuarioRegional item);

        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> CreateUsuarioRegional(SGPA.Server.Models.CMU.UsuarioRegional usuarioregional)
        {
            OnUsuarioRegionalCreated(usuarioregional);

            var existingItem = Context.UsuarioRegionals
                              .Where(i => i.Oid == usuarioregional.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.UsuarioRegionals.Add(usuarioregional);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(usuarioregional).State = EntityState.Detached;
                throw;
            }

            OnAfterUsuarioRegionalCreated(usuarioregional);

            return usuarioregional;
        }

        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> CancelUsuarioRegionalChanges(SGPA.Server.Models.CMU.UsuarioRegional item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUsuarioRegionalUpdated(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnAfterUsuarioRegionalUpdated(SGPA.Server.Models.CMU.UsuarioRegional item);

        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> UpdateUsuarioRegional(Guid oid, SGPA.Server.Models.CMU.UsuarioRegional usuarioregional)
        {
            OnUsuarioRegionalUpdated(usuarioregional);

            var itemToUpdate = Context.UsuarioRegionals
                              .Where(i => i.Oid == usuarioregional.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(usuarioregional);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUsuarioRegionalUpdated(usuarioregional);

            return usuarioregional;
        }

        partial void OnUsuarioRegionalDeleted(SGPA.Server.Models.CMU.UsuarioRegional item);
        partial void OnAfterUsuarioRegionalDeleted(SGPA.Server.Models.CMU.UsuarioRegional item);

        public async Task<SGPA.Server.Models.CMU.UsuarioRegional> DeleteUsuarioRegional(Guid oid)
        {
            var itemToDelete = Context.UsuarioRegionals
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsuarioRegionalDeleted(itemToDelete);


            Context.UsuarioRegionals.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUsuarioRegionalDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpObjectModifiedsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjectmodifieds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjectmodifieds/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpObjectModifiedsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjectmodifieds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjectmodifieds/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpObjectModifiedsRead(ref IQueryable<SGPA.Server.Models.CMU.XpObjectModified> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpObjectModified>> GetXpObjectModifieds(Query query = null)
        {
            var items = Context.XpObjectModifieds.AsQueryable();


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

            OnXpObjectModifiedsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpObjectModifiedGet(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnGetXpObjectModifiedById(ref IQueryable<SGPA.Server.Models.CMU.XpObjectModified> items);


        public async Task<SGPA.Server.Models.CMU.XpObjectModified> GetXpObjectModifiedById(int id)
        {
            var items = Context.XpObjectModifieds
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetXpObjectModifiedById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpObjectModifiedGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpObjectModifiedCreated(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnAfterXpObjectModifiedCreated(SGPA.Server.Models.CMU.XpObjectModified item);

        public async Task<SGPA.Server.Models.CMU.XpObjectModified> CreateXpObjectModified(SGPA.Server.Models.CMU.XpObjectModified xpobjectmodified)
        {
            OnXpObjectModifiedCreated(xpobjectmodified);

            var existingItem = Context.XpObjectModifieds
                              .Where(i => i.Id == xpobjectmodified.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpObjectModifieds.Add(xpobjectmodified);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpobjectmodified).State = EntityState.Detached;
                throw;
            }

            OnAfterXpObjectModifiedCreated(xpobjectmodified);

            return xpobjectmodified;
        }

        public async Task<SGPA.Server.Models.CMU.XpObjectModified> CancelXpObjectModifiedChanges(SGPA.Server.Models.CMU.XpObjectModified item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpObjectModifiedUpdated(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnAfterXpObjectModifiedUpdated(SGPA.Server.Models.CMU.XpObjectModified item);

        public async Task<SGPA.Server.Models.CMU.XpObjectModified> UpdateXpObjectModified(int id, SGPA.Server.Models.CMU.XpObjectModified xpobjectmodified)
        {
            OnXpObjectModifiedUpdated(xpobjectmodified);

            var itemToUpdate = Context.XpObjectModifieds
                              .Where(i => i.Id == xpobjectmodified.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpobjectmodified);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpObjectModifiedUpdated(xpobjectmodified);

            return xpobjectmodified;
        }

        partial void OnXpObjectModifiedDeleted(SGPA.Server.Models.CMU.XpObjectModified item);
        partial void OnAfterXpObjectModifiedDeleted(SGPA.Server.Models.CMU.XpObjectModified item);

        public async Task<SGPA.Server.Models.CMU.XpObjectModified> DeleteXpObjectModified(int id)
        {
            var itemToDelete = Context.XpObjectModifieds
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpObjectModifiedDeleted(itemToDelete);


            Context.XpObjectModifieds.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpObjectModifiedDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpObjectTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjecttypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjecttypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpObjectTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpobjecttypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpobjecttypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpObjectTypesRead(ref IQueryable<SGPA.Server.Models.CMU.XpObjectType> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpObjectType>> GetXpObjectTypes(Query query = null)
        {
            var items = Context.XpObjectTypes.AsQueryable();


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

            OnXpObjectTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpObjectTypeGet(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnGetXpObjectTypeByOid(ref IQueryable<SGPA.Server.Models.CMU.XpObjectType> items);


        public async Task<SGPA.Server.Models.CMU.XpObjectType> GetXpObjectTypeByOid(int oid)
        {
            var items = Context.XpObjectTypes
                              .AsNoTracking()
                              .Where(i => i.OID == oid);

 
            OnGetXpObjectTypeByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpObjectTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpObjectTypeCreated(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnAfterXpObjectTypeCreated(SGPA.Server.Models.CMU.XpObjectType item);

        public async Task<SGPA.Server.Models.CMU.XpObjectType> CreateXpObjectType(SGPA.Server.Models.CMU.XpObjectType xpobjecttype)
        {
            OnXpObjectTypeCreated(xpobjecttype);

            var existingItem = Context.XpObjectTypes
                              .Where(i => i.OID == xpobjecttype.OID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpObjectTypes.Add(xpobjecttype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpobjecttype).State = EntityState.Detached;
                throw;
            }

            OnAfterXpObjectTypeCreated(xpobjecttype);

            return xpobjecttype;
        }

        public async Task<SGPA.Server.Models.CMU.XpObjectType> CancelXpObjectTypeChanges(SGPA.Server.Models.CMU.XpObjectType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpObjectTypeUpdated(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnAfterXpObjectTypeUpdated(SGPA.Server.Models.CMU.XpObjectType item);

        public async Task<SGPA.Server.Models.CMU.XpObjectType> UpdateXpObjectType(int oid, SGPA.Server.Models.CMU.XpObjectType xpobjecttype)
        {
            OnXpObjectTypeUpdated(xpobjecttype);

            var itemToUpdate = Context.XpObjectTypes
                              .Where(i => i.OID == xpobjecttype.OID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpobjecttype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpObjectTypeUpdated(xpobjecttype);

            return xpobjecttype;
        }

        partial void OnXpObjectTypeDeleted(SGPA.Server.Models.CMU.XpObjectType item);
        partial void OnAfterXpObjectTypeDeleted(SGPA.Server.Models.CMU.XpObjectType item);

        public async Task<SGPA.Server.Models.CMU.XpObjectType> DeleteXpObjectType(int oid)
        {
            var itemToDelete = Context.XpObjectTypes
                              .Where(i => i.OID == oid)
                              .Include(i => i.Cobros)
                              .Include(i => i.CobroNominas)
                              .Include(i => i.ColegiadoBitacoras)
                              .Include(i => i.ColegiadoDeclaracionJurada)
                              .Include(i => i.Convenios)
                              .Include(i => i.MovimientoCuenta)
                              .Include(i => i.OrigenMovimientos)
                              .Include(i => i.SecuritySystemRoles)
                              .Include(i => i.SecuritySystemTypePermissionsObjects)
                              .Include(i => i.SecuritySystemUsers)
                              .Include(i => i.TramiteInfoadjuntabases)
                              .Include(i => i.XpObjectModifieds)
                              .Include(i => i.XpWeakReferences)
                              .Include(i => i.XpWeakReferences1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpObjectTypeDeleted(itemToDelete);


            Context.XpObjectTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpObjectTypeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpoStatesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostates/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpoStatesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostates/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpoStatesRead(ref IQueryable<SGPA.Server.Models.CMU.XpoState> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpoState>> GetXpoStates(Query query = null)
        {
            var items = Context.XpoStates.AsQueryable();

            items = items.Include(i => i.XpoStateMachine);

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

            OnXpoStatesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpoStateGet(SGPA.Server.Models.CMU.XpoState item);
        partial void OnGetXpoStateByOid(ref IQueryable<SGPA.Server.Models.CMU.XpoState> items);


        public async Task<SGPA.Server.Models.CMU.XpoState> GetXpoStateByOid(Guid oid)
        {
            var items = Context.XpoStates
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.XpoStateMachine);
 
            OnGetXpoStateByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpoStateGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpoStateCreated(SGPA.Server.Models.CMU.XpoState item);
        partial void OnAfterXpoStateCreated(SGPA.Server.Models.CMU.XpoState item);

        public async Task<SGPA.Server.Models.CMU.XpoState> CreateXpoState(SGPA.Server.Models.CMU.XpoState xpostate)
        {
            OnXpoStateCreated(xpostate);

            var existingItem = Context.XpoStates
                              .Where(i => i.Oid == xpostate.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpoStates.Add(xpostate);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpostate).State = EntityState.Detached;
                throw;
            }

            OnAfterXpoStateCreated(xpostate);

            return xpostate;
        }

        public async Task<SGPA.Server.Models.CMU.XpoState> CancelXpoStateChanges(SGPA.Server.Models.CMU.XpoState item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpoStateUpdated(SGPA.Server.Models.CMU.XpoState item);
        partial void OnAfterXpoStateUpdated(SGPA.Server.Models.CMU.XpoState item);

        public async Task<SGPA.Server.Models.CMU.XpoState> UpdateXpoState(Guid oid, SGPA.Server.Models.CMU.XpoState xpostate)
        {
            OnXpoStateUpdated(xpostate);

            var itemToUpdate = Context.XpoStates
                              .Where(i => i.Oid == xpostate.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpostate);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpoStateUpdated(xpostate);

            return xpostate;
        }

        partial void OnXpoStateDeleted(SGPA.Server.Models.CMU.XpoState item);
        partial void OnAfterXpoStateDeleted(SGPA.Server.Models.CMU.XpoState item);

        public async Task<SGPA.Server.Models.CMU.XpoState> DeleteXpoState(Guid oid)
        {
            var itemToDelete = Context.XpoStates
                              .Where(i => i.Oid == oid)
                              .Include(i => i.XpoStateAppearances)
                              .Include(i => i.XpoStateMachines)
                              .Include(i => i.XpoTransitions)
                              .Include(i => i.XpoTransitions1)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpoStateDeleted(itemToDelete);


            Context.XpoStates.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpoStateDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpoStateAppearancesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostateappearances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostateappearances/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpoStateAppearancesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostateappearances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostateappearances/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpoStateAppearancesRead(ref IQueryable<SGPA.Server.Models.CMU.XpoStateAppearance> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpoStateAppearance>> GetXpoStateAppearances(Query query = null)
        {
            var items = Context.XpoStateAppearances.AsQueryable();

            items = items.Include(i => i.XpoState);

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

            OnXpoStateAppearancesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpoStateAppearanceGet(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnGetXpoStateAppearanceByOid(ref IQueryable<SGPA.Server.Models.CMU.XpoStateAppearance> items);


        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> GetXpoStateAppearanceByOid(Guid oid)
        {
            var items = Context.XpoStateAppearances
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.XpoState);
 
            OnGetXpoStateAppearanceByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpoStateAppearanceGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpoStateAppearanceCreated(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnAfterXpoStateAppearanceCreated(SGPA.Server.Models.CMU.XpoStateAppearance item);

        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> CreateXpoStateAppearance(SGPA.Server.Models.CMU.XpoStateAppearance xpostateappearance)
        {
            OnXpoStateAppearanceCreated(xpostateappearance);

            var existingItem = Context.XpoStateAppearances
                              .Where(i => i.Oid == xpostateappearance.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpoStateAppearances.Add(xpostateappearance);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpostateappearance).State = EntityState.Detached;
                throw;
            }

            OnAfterXpoStateAppearanceCreated(xpostateappearance);

            return xpostateappearance;
        }

        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> CancelXpoStateAppearanceChanges(SGPA.Server.Models.CMU.XpoStateAppearance item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpoStateAppearanceUpdated(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnAfterXpoStateAppearanceUpdated(SGPA.Server.Models.CMU.XpoStateAppearance item);

        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> UpdateXpoStateAppearance(Guid oid, SGPA.Server.Models.CMU.XpoStateAppearance xpostateappearance)
        {
            OnXpoStateAppearanceUpdated(xpostateappearance);

            var itemToUpdate = Context.XpoStateAppearances
                              .Where(i => i.Oid == xpostateappearance.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpostateappearance);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpoStateAppearanceUpdated(xpostateappearance);

            return xpostateappearance;
        }

        partial void OnXpoStateAppearanceDeleted(SGPA.Server.Models.CMU.XpoStateAppearance item);
        partial void OnAfterXpoStateAppearanceDeleted(SGPA.Server.Models.CMU.XpoStateAppearance item);

        public async Task<SGPA.Server.Models.CMU.XpoStateAppearance> DeleteXpoStateAppearance(Guid oid)
        {
            var itemToDelete = Context.XpoStateAppearances
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpoStateAppearanceDeleted(itemToDelete);


            Context.XpoStateAppearances.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpoStateAppearanceDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpoStateMachinesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostatemachines/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostatemachines/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpoStateMachinesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpostatemachines/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpostatemachines/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpoStateMachinesRead(ref IQueryable<SGPA.Server.Models.CMU.XpoStateMachine> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpoStateMachine>> GetXpoStateMachines(Query query = null)
        {
            var items = Context.XpoStateMachines.AsQueryable();

            items = items.Include(i => i.XpoState);

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

            OnXpoStateMachinesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpoStateMachineGet(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnGetXpoStateMachineByOid(ref IQueryable<SGPA.Server.Models.CMU.XpoStateMachine> items);


        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> GetXpoStateMachineByOid(Guid oid)
        {
            var items = Context.XpoStateMachines
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.XpoState);
 
            OnGetXpoStateMachineByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpoStateMachineGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpoStateMachineCreated(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnAfterXpoStateMachineCreated(SGPA.Server.Models.CMU.XpoStateMachine item);

        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> CreateXpoStateMachine(SGPA.Server.Models.CMU.XpoStateMachine xpostatemachine)
        {
            OnXpoStateMachineCreated(xpostatemachine);

            var existingItem = Context.XpoStateMachines
                              .Where(i => i.Oid == xpostatemachine.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpoStateMachines.Add(xpostatemachine);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpostatemachine).State = EntityState.Detached;
                throw;
            }

            OnAfterXpoStateMachineCreated(xpostatemachine);

            return xpostatemachine;
        }

        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> CancelXpoStateMachineChanges(SGPA.Server.Models.CMU.XpoStateMachine item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpoStateMachineUpdated(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnAfterXpoStateMachineUpdated(SGPA.Server.Models.CMU.XpoStateMachine item);

        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> UpdateXpoStateMachine(Guid oid, SGPA.Server.Models.CMU.XpoStateMachine xpostatemachine)
        {
            OnXpoStateMachineUpdated(xpostatemachine);

            var itemToUpdate = Context.XpoStateMachines
                              .Where(i => i.Oid == xpostatemachine.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpostatemachine);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpoStateMachineUpdated(xpostatemachine);

            return xpostatemachine;
        }

        partial void OnXpoStateMachineDeleted(SGPA.Server.Models.CMU.XpoStateMachine item);
        partial void OnAfterXpoStateMachineDeleted(SGPA.Server.Models.CMU.XpoStateMachine item);

        public async Task<SGPA.Server.Models.CMU.XpoStateMachine> DeleteXpoStateMachine(Guid oid)
        {
            var itemToDelete = Context.XpoStateMachines
                              .Where(i => i.Oid == oid)
                              .Include(i => i.XpoStates)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpoStateMachineDeleted(itemToDelete);


            Context.XpoStateMachines.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpoStateMachineDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpoTransitionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpotransitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpotransitions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpoTransitionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpotransitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpotransitions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpoTransitionsRead(ref IQueryable<SGPA.Server.Models.CMU.XpoTransition> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpoTransition>> GetXpoTransitions(Query query = null)
        {
            var items = Context.XpoTransitions.AsQueryable();

            items = items.Include(i => i.XpoState);
            items = items.Include(i => i.XpoState1);

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

            OnXpoTransitionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpoTransitionGet(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnGetXpoTransitionByOid(ref IQueryable<SGPA.Server.Models.CMU.XpoTransition> items);


        public async Task<SGPA.Server.Models.CMU.XpoTransition> GetXpoTransitionByOid(Guid oid)
        {
            var items = Context.XpoTransitions
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

            items = items.Include(i => i.XpoState);
            items = items.Include(i => i.XpoState1);
 
            OnGetXpoTransitionByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpoTransitionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpoTransitionCreated(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnAfterXpoTransitionCreated(SGPA.Server.Models.CMU.XpoTransition item);

        public async Task<SGPA.Server.Models.CMU.XpoTransition> CreateXpoTransition(SGPA.Server.Models.CMU.XpoTransition xpotransition)
        {
            OnXpoTransitionCreated(xpotransition);

            var existingItem = Context.XpoTransitions
                              .Where(i => i.Oid == xpotransition.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpoTransitions.Add(xpotransition);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpotransition).State = EntityState.Detached;
                throw;
            }

            OnAfterXpoTransitionCreated(xpotransition);

            return xpotransition;
        }

        public async Task<SGPA.Server.Models.CMU.XpoTransition> CancelXpoTransitionChanges(SGPA.Server.Models.CMU.XpoTransition item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpoTransitionUpdated(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnAfterXpoTransitionUpdated(SGPA.Server.Models.CMU.XpoTransition item);

        public async Task<SGPA.Server.Models.CMU.XpoTransition> UpdateXpoTransition(Guid oid, SGPA.Server.Models.CMU.XpoTransition xpotransition)
        {
            OnXpoTransitionUpdated(xpotransition);

            var itemToUpdate = Context.XpoTransitions
                              .Where(i => i.Oid == xpotransition.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpotransition);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpoTransitionUpdated(xpotransition);

            return xpotransition;
        }

        partial void OnXpoTransitionDeleted(SGPA.Server.Models.CMU.XpoTransition item);
        partial void OnAfterXpoTransitionDeleted(SGPA.Server.Models.CMU.XpoTransition item);

        public async Task<SGPA.Server.Models.CMU.XpoTransition> DeleteXpoTransition(Guid oid)
        {
            var itemToDelete = Context.XpoTransitions
                              .Where(i => i.Oid == oid)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpoTransitionDeleted(itemToDelete);


            Context.XpoTransitions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpoTransitionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportXpWeakReferencesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpweakreferences/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportXpWeakReferencesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/cmu/xpweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/cmu/xpweakreferences/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnXpWeakReferencesRead(ref IQueryable<SGPA.Server.Models.CMU.XpWeakReference> items);

        public async Task<IQueryable<SGPA.Server.Models.CMU.XpWeakReference>> GetXpWeakReferences(Query query = null)
        {
            var items = Context.XpWeakReferences.AsQueryable();


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

            OnXpWeakReferencesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnXpWeakReferenceGet(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnGetXpWeakReferenceByOid(ref IQueryable<SGPA.Server.Models.CMU.XpWeakReference> items);


        public async Task<SGPA.Server.Models.CMU.XpWeakReference> GetXpWeakReferenceByOid(Guid oid)
        {
            var items = Context.XpWeakReferences
                              .AsNoTracking()
                              .Where(i => i.Oid == oid);

 
            OnGetXpWeakReferenceByOid(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnXpWeakReferenceGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnXpWeakReferenceCreated(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnAfterXpWeakReferenceCreated(SGPA.Server.Models.CMU.XpWeakReference item);

        public async Task<SGPA.Server.Models.CMU.XpWeakReference> CreateXpWeakReference(SGPA.Server.Models.CMU.XpWeakReference xpweakreference)
        {
            OnXpWeakReferenceCreated(xpweakreference);

            var existingItem = Context.XpWeakReferences
                              .Where(i => i.Oid == xpweakreference.Oid)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.XpWeakReferences.Add(xpweakreference);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(xpweakreference).State = EntityState.Detached;
                throw;
            }

            OnAfterXpWeakReferenceCreated(xpweakreference);

            return xpweakreference;
        }

        public async Task<SGPA.Server.Models.CMU.XpWeakReference> CancelXpWeakReferenceChanges(SGPA.Server.Models.CMU.XpWeakReference item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnXpWeakReferenceUpdated(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnAfterXpWeakReferenceUpdated(SGPA.Server.Models.CMU.XpWeakReference item);

        public async Task<SGPA.Server.Models.CMU.XpWeakReference> UpdateXpWeakReference(Guid oid, SGPA.Server.Models.CMU.XpWeakReference xpweakreference)
        {
            OnXpWeakReferenceUpdated(xpweakreference);

            var itemToUpdate = Context.XpWeakReferences
                              .Where(i => i.Oid == xpweakreference.Oid)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(xpweakreference);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterXpWeakReferenceUpdated(xpweakreference);

            return xpweakreference;
        }

        partial void OnXpWeakReferenceDeleted(SGPA.Server.Models.CMU.XpWeakReference item);
        partial void OnAfterXpWeakReferenceDeleted(SGPA.Server.Models.CMU.XpWeakReference item);

        public async Task<SGPA.Server.Models.CMU.XpWeakReference> DeleteXpWeakReference(Guid oid)
        {
            var itemToDelete = Context.XpWeakReferences
                              .Where(i => i.Oid == oid)
                              .Include(i => i.AuditDataItemPersistents)
                              .Include(i => i.AuditDataItemPersistents1)
                              .Include(i => i.AuditedObjectWeakReferences)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnXpWeakReferenceDeleted(itemToDelete);


            Context.XpWeakReferences.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterXpWeakReferenceDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}