﻿using System.Collections.Generic;
using System.Text;
using Contpaqi.Sdk.Extras.Helpers;
using Contpaqi.Sdk.Extras.Interfaces;
using Contpaqi.Sdk.Extras.Models;
using Contpaqi.Sdk.Extras.Models.Enums;

namespace Contpaqi.Sdk.Extras.Repositories
{
    public class ClienteProveedorRepository : IClienteProveedorRepository<ClienteProveedor>
    {
        private readonly IContpaqiSdk _sdk;
        private readonly IValorClasificacionRepository<ValorClasificacion> _valorClasificacionRepository;
        private readonly IMonedaRepository<Moneda> _monedaRepository;
        private readonly IAgenteRepository<Agente> _agenteRepository;
        private readonly IAlmacenRepository<Almacen> _almacenRepository;
        public ClienteProveedorRepository(IContpaqiSdk sdk)
        {
            _sdk = sdk;
            _valorClasificacionRepository = new ValorClasificacionRepository(sdk);
            _monedaRepository = new MonedaRepository();
            _agenteRepository = new AgenteRepository(sdk);
            _almacenRepository = new AlmacenRepository(sdk);
        }

        public ClienteProveedor BuscarPorId(int idCliente)
        {
            return _sdk.fBuscaIdCteProv(idCliente) == SdkResultConstants.Success ? LeerDatosClienteProveedorActual() : null;
        }

        public ClienteProveedor BuscarPorCodigo(string codigoCliente)
        {
            return _sdk.fBuscaCteProv(codigoCliente) == SdkResultConstants.Success ? LeerDatosClienteProveedorActual() : null;
        }

        public IEnumerable<ClienteProveedor> TraerPorTipo(TipoClienteEnum tipoCliente)
        {
            _sdk.fPosPrimerCteProv().ToResultadoSdk(_sdk).ThrowIfError();
            var tipoDeCliente = new StringBuilder(7);
            _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoDeCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            if (tipoCliente == TipoClienteHelper.ConvertToTipoClienteEnum(tipoDeCliente.ToString()))
            {
                yield return LeerDatosClienteProveedorActual();
            }

            while (_sdk.fPosSiguienteCteProv() == SdkResultConstants.Success)
            {
                _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoDeCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
                if (tipoCliente == TipoClienteHelper.ConvertToTipoClienteEnum(tipoDeCliente.ToString()))
                {
                    yield return LeerDatosClienteProveedorActual();
                }

                if (_sdk.fPosEOFCteProv() == 1)
                {
                    break;
                }
            }
        }

        public IEnumerable<ClienteProveedor> TraerTodo()
        {
            _sdk.fPosPrimerCteProv().ToResultadoSdk(_sdk).ThrowIfError();
            yield return LeerDatosClienteProveedorActual();
            while (_sdk.fPosSiguienteCteProv() == SdkResultConstants.Success)
            {
                yield return LeerDatosClienteProveedorActual();
                if (_sdk.fPosEOFCteProv() == 1)
                {
                    break;
                }
            }
        }

        public IEnumerable<ClienteProveedor> TraerClientes()
        {
            var tipoDeCliente = new StringBuilder(7);

            _sdk.fPosPrimerCteProv().ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoDeCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            if (TipoClienteHelper.IsCliente(tipoDeCliente.ToString()))
            {
                yield return LeerDatosClienteProveedorActual();
            }

            while (_sdk.fPosSiguienteCteProv() == SdkResultConstants.Success)
            {
                _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoDeCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
                if (TipoClienteHelper.IsCliente(tipoDeCliente.ToString()))
                {
                    yield return LeerDatosClienteProveedorActual();
                }

                if (_sdk.fPosEOFCteProv() == 1)
                {
                    break;
                }
            }
        }

        public IEnumerable<ClienteProveedor> TraerProveedores()
        {
            var tipoDeCliente = new StringBuilder(7);

            _sdk.fPosPrimerCteProv().ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoDeCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            if (TipoClienteHelper.IsProveedor(tipoDeCliente.ToString()))
            {
                yield return LeerDatosClienteProveedorActual();
            }

            while (_sdk.fPosSiguienteCteProv() == SdkResultConstants.Success)
            {
                _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoDeCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
                if (TipoClienteHelper.IsProveedor(tipoDeCliente.ToString()))
                {
                    yield return LeerDatosClienteProveedorActual();
                }

                if (_sdk.fPosEOFCteProv() == 1)
                {
                    break;
                }
            }
        }

        private ClienteProveedor LeerDatosClienteProveedorActual()
        {
            var codigo = new StringBuilder(Constantes.kLongCodigo);
            var razonSocial = new StringBuilder(Constantes.kLongNombre);
            var fechaAlta = new StringBuilder(9);
            var rfc = new StringBuilder(Constantes.kLongRFC);
            var curp = new StringBuilder(21);
            var denominacionComercial = new StringBuilder(51);
            var representanteLegal = new StringBuilder(51);
            var idMoneda = new StringBuilder(12);
            var listaPreciosCliente = new StringBuilder(7);
            var descuentoMovimiento = new StringBuilder(9);
            var banderaVentaCredito = new StringBuilder(7);
            var idValorClasificacionCliente1 = new StringBuilder(12);
            var idValorClasificacionCliente2 = new StringBuilder(12);
            var idValorClasificacionCliente3 = new StringBuilder(12);
            var idValorClasificacionCliente4 = new StringBuilder(12);
            var idValorClasificacionCliente5 = new StringBuilder(12);
            var idValorClasificacionCliente6 = new StringBuilder(12);
            var tipoCliente = new StringBuilder(7);
            var estatus = new StringBuilder(7);
            var fechaBaja = new StringBuilder(9);
            var fechaUltimaRevision = new StringBuilder(9);
            var limiteCreditoCliente = new StringBuilder(9);
            var diasCreditoCliente = new StringBuilder(12);
            var banderaExcederCredito = new StringBuilder(7);
            var descuentoProntoPago = new StringBuilder(9);
            var diasProntoPago = new StringBuilder(12);
            var interesMoratorio = new StringBuilder(9);
            var diaPago = new StringBuilder(7);
            var diasRevision = new StringBuilder(7);
            var mensajeria = new StringBuilder(21);
            var cuentaMensajeria = new StringBuilder(61);
            var diasEmbarqueCliente = new StringBuilder(7);
            var idAlmacen = new StringBuilder(12);
            var idAgenteVenta = new StringBuilder(12);
            var idAgenteCobro = new StringBuilder(12);
            var comisionVenta = new StringBuilder(9);
            var comisionCobro = new StringBuilder(9);
            var restriccionAgente = new StringBuilder(7);
            var impuesto1 = new StringBuilder(9);
            var impuesto2 = new StringBuilder(9);
            var impuesto3 = new StringBuilder(9);
            var retencionCliente1 = new StringBuilder(9);
            var retencionCliente2 = new StringBuilder(9);
            var idValorClasificacionProveedor1 = new StringBuilder(12);
            var idValorClasificacionProveedor2 = new StringBuilder(12);
            var idValorClasificacionProveedor3 = new StringBuilder(12);
            var idValorClasificacionProveedor4 = new StringBuilder(12);
            var idValorClasificacionProveedor5 = new StringBuilder(12);
            var idValorClasificacionProveedor6 = new StringBuilder(12);
            var limiteCreditoProveedor = new StringBuilder(9);
            var diasCreditoProveedor = new StringBuilder(12);
            var tiempoEntrega = new StringBuilder(12);
            var diasEmbarqueProveedor = new StringBuilder(7);
            var impuestoProveedor1 = new StringBuilder(9);
            var impuestoProveedor2 = new StringBuilder(9);
            var impuestoProveedor3 = new StringBuilder(9);
            var retencionProveedor1 = new StringBuilder(9);
            var retencionProveedor2 = new StringBuilder(9);
            var banderaInteresMoratorio = new StringBuilder(7);
            var textoExtra1 = new StringBuilder(51);
            var textoExtra2 = new StringBuilder(51);
            var textoExtra3 = new StringBuilder(51);
            var importeExtra1 = new StringBuilder(9);
            var importeExtra2 = new StringBuilder(9);
            var importeExtra3 = new StringBuilder(9);
            var importeExtra4 = new StringBuilder(9);
            var id = new StringBuilder(12);
            var nombreLargo = new StringBuilder(254);
            var email1 = new StringBuilder(60);
            var email2 = new StringBuilder(60);
            var email3 = new StringBuilder(60);
            var formaPago = new StringBuilder(101);
            var numeroCuentaPago = new StringBuilder(101);
            var usoCfdi = new StringBuilder(31);

            _sdk.fLeeDatoCteProv("CCODIGOCLIENTE", codigo, Constantes.kLongCodigo).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRAZONSOCIAL", razonSocial, Constantes.kLongNombre).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CFECHAALTA", fechaAlta, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRFC", rfc, Constantes.kLongRFC).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CCURP", curp, 21).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDENCOMERCIAL", denominacionComercial, 51).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CREPLEGAL", representanteLegal, 51).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDMONEDA", idMoneda, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CLISTAPRECIOCLIENTE", listaPreciosCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDESCUENTOMOVTO", descuentoMovimiento, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CBANVENTACREDITO", banderaVentaCredito, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFCLIENTE1", idValorClasificacionCliente1, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFCLIENTE2", idValorClasificacionCliente2, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFCLIENTE3", idValorClasificacionCliente3, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFCLIENTE4", idValorClasificacionCliente4, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFCLIENTE5", idValorClasificacionCliente5, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFCLIENTE6", idValorClasificacionCliente6, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTIPOCLIENTE", tipoCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CESTATUS", estatus, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CFECHABAJA", fechaBaja, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CFECHAULTIMAREVISION", fechaUltimaRevision, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CLIMITECREDITOCLIENTE", limiteCreditoCliente, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIASCREDITOCLIENTE", diasCreditoCliente, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CBANEXCEDERCREDITO", banderaExcederCredito, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDESCUENTOPRONTOPAGO", descuentoProntoPago, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIASPRONTOPAGO", diasProntoPago, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CINTERESMORATORIO", interesMoratorio, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIAPAGO", diaPago, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIASREVISION", diasRevision, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CMENSAJERIA", mensajeria, 21).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CCUENTAMENSAJERIA", cuentaMensajeria, 61).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIASEMBARQUECLIENTE", diasEmbarqueCliente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDALMACEN", idAlmacen, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDAGENTEVENTA", idAgenteVenta, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDAGENTECOBRO", idAgenteCobro, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CCOMVENTA", comisionVenta, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CCOMCOBRO", comisionCobro, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRESTRICCIONAGENTE", restriccionAgente, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPUESTO1", impuesto1, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPUESTO2", impuesto2, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPUESTO3", impuesto3, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRETENCIONCLIENTE1", retencionCliente1, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRETENCIONCLIENTE2", retencionCliente2, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFPROVEEDOR1", idValorClasificacionProveedor1, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFPROVEEDOR2", idValorClasificacionProveedor2, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFPROVEEDOR3", idValorClasificacionProveedor3, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFPROVEEDOR4", idValorClasificacionProveedor4, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFPROVEEDOR5", idValorClasificacionProveedor5, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDVALORCLASIFPROVEEDOR6", idValorClasificacionProveedor6, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CLIMITECREDITOPROVEEDOR", limiteCreditoProveedor, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIASCREDITOPROVEEDOR", diasCreditoProveedor, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTIEMPOENTREGA", tiempoEntrega, 12).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CDIASEMBARQUEPROVEEDOR", diasEmbarqueProveedor, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPUESTOPROVEEDOR1", impuestoProveedor1, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPUESTOPROVEEDOR2", impuestoProveedor2, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPUESTOPROVEEDOR3", impuestoProveedor3, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRETENCIONPROVEEDOR1", retencionProveedor1, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CRETENCIONPROVEEDOR2", retencionProveedor2, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CBANINTERESMORATORIO", banderaInteresMoratorio, 7).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTEXTOEXTRA1", textoExtra1, 51).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTEXTOEXTRA2", textoExtra2, 51).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CTEXTOEXTRA3", textoExtra3, 51).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPORTEEXTRA1", importeExtra1, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPORTEEXTRA2", importeExtra2, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPORTEEXTRA3", importeExtra3, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIMPORTEEXTRA4", importeExtra4, 9).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CIDCLIENTEPROVEEDOR", id, 12).ToResultadoSdk(_sdk).ThrowIfError();

            var sdkResult = _sdk.fLeeDatoCteProv("CNOMBRELARGO", nombreLargo, 254).ToResultadoSdk(_sdk); // Al parecer solo los clientes pueden tener un nombre largo, pero hay clientes que tienen su nombre largo vacio por lo que tambien causa error envez de retornar un string vacio
            if (!sdkResult.IsSuccess && sdkResult.Result != 3) // Si el resultado es 3 quiere decir que no enonctro un mensaje de error por que esta vacio o nulo. Si el resultado es diferente arrojar un error.
            {
                sdkResult.ThrowIfError();
            }

            _sdk.fLeeDatoCteProv("CEMAIL1", email1, 60).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CEMAIL2", email2, 60).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CEMAIL3", email3, 60).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CMETODOPAG", formaPago, 101).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CNUMCTAPAG", numeroCuentaPago, 101).ToResultadoSdk(_sdk).ThrowIfError();
            _sdk.fLeeDatoCteProv("CUSOCFDI", usoCfdi, 31).ToResultadoSdk(_sdk).ThrowIfError();

            var clienteProveedor = new ClienteProveedor();
            clienteProveedor.Codigo = codigo.ToString();
            clienteProveedor.RazonSocial = razonSocial.ToString();
            clienteProveedor.FechaAlta = SdkDateTimeHelper.FromSdkCatalogoString(fechaAlta.ToString());
            clienteProveedor.Rfc = rfc.ToString();
            clienteProveedor.Curp = curp.ToString();
            clienteProveedor.DenominacionComercial = denominacionComercial.ToString();
            clienteProveedor.RepresentanteLegal = representanteLegal.ToString();
            clienteProveedor.IdMoneda = int.Parse(idMoneda.ToString());
            clienteProveedor.ListaPreciosCliente = int.Parse(listaPreciosCliente.ToString());
            clienteProveedor.DescuentoMovimiento = double.Parse(descuentoMovimiento.ToString());
            clienteProveedor.BanderaVentaCredito = int.TryParse(banderaVentaCredito.ToString(), out var banderaVentaCreditoResult) ? banderaVentaCreditoResult : 0;
            clienteProveedor.IdValorClasificacionCliente1 = int.TryParse(idValorClasificacionCliente1.ToString(), out var idValorClasificacionCliente1Result) ? idValorClasificacionCliente1Result : 0;
            clienteProveedor.IdValorClasificacionCliente2 = int.TryParse(idValorClasificacionCliente2.ToString(), out var idValorClasificacionCliente2Result) ? idValorClasificacionCliente2Result : 0;
            clienteProveedor.IdValorClasificacionCliente3 = int.TryParse(idValorClasificacionCliente3.ToString(), out var idValorClasificacionCliente3Result) ? idValorClasificacionCliente3Result : 0;
            clienteProveedor.IdValorClasificacionCliente4 = int.TryParse(idValorClasificacionCliente4.ToString(), out var idValorClasificacionCliente4Result) ? idValorClasificacionCliente4Result : 0;
            clienteProveedor.IdValorClasificacionCliente5 = int.TryParse(idValorClasificacionCliente5.ToString(), out var idValorClasificacionCliente5Result) ? idValorClasificacionCliente5Result : 0;
            clienteProveedor.IdValorClasificacionCliente6 = int.TryParse(idValorClasificacionCliente6.ToString(), out var idValorClasificacionCliente6Result) ? idValorClasificacionCliente6Result : 0;
            clienteProveedor.Tipo = TipoClienteHelper.ConvertToTipoClienteEnum(tipoCliente.ToString());
            clienteProveedor.Estatus = EstatusActivoEnumHelper.ConvertToEstatusActivoEnum(estatus.ToString());
            clienteProveedor.FechaBaja = SdkDateTimeHelper.FromSdkCatalogoString(fechaBaja.ToString());
            clienteProveedor.FechaUltimaRevision = SdkDateTimeHelper.FromSdkCatalogoString(fechaUltimaRevision.ToString());
            clienteProveedor.LimiteCreditoCliente = double.Parse(limiteCreditoCliente.ToString());
            clienteProveedor.DiasCreditoCliente = int.TryParse(diasCreditoCliente.ToString(), out var diasCreditoClienteResult) ? diasCreditoClienteResult : 0;
            clienteProveedor.BanderaExcederCredito = int.TryParse(banderaExcederCredito.ToString(), out var banderaExcederCreditoResult) ? banderaExcederCreditoResult : 0;
            clienteProveedor.DescuentoProntoPago = double.Parse(descuentoProntoPago.ToString());
            clienteProveedor.DiasProntoPago = int.TryParse(diasProntoPago.ToString(), out var diasProntoPagoResult) ? diasProntoPagoResult : 0;
            clienteProveedor.InteresMoratorio = double.Parse(interesMoratorio.ToString());
            clienteProveedor.DiaPago = int.Parse(diaPago.ToString());
            clienteProveedor.DiasRevision = int.Parse(diasRevision.ToString());
            clienteProveedor.Mensajeria = mensajeria.ToString();
            clienteProveedor.CuentaMensajeria = cuentaMensajeria.ToString();
            clienteProveedor.DiasEmbarqueCliente = int.Parse(diasEmbarqueCliente.ToString());
            clienteProveedor.IdAlmacen = int.Parse(idAlmacen.ToString());
            clienteProveedor.IdAgenteVenta = int.Parse(idAgenteVenta.ToString());
            clienteProveedor.IdAgenteCobro = int.Parse(idAgenteCobro.ToString());
            clienteProveedor.ComisionVenta = double.Parse(comisionVenta.ToString());
            clienteProveedor.ComisionCobro = double.Parse(comisionCobro.ToString());
            clienteProveedor.RestriccionAgente = int.TryParse(restriccionAgente.ToString(), out var restriccionAgenteResult) ? restriccionAgenteResult : 0;
            clienteProveedor.Impuesto1 = double.Parse(impuesto1.ToString());
            clienteProveedor.Impuesto2 = double.Parse(impuesto2.ToString());
            clienteProveedor.Impuesto3 = double.Parse(impuesto3.ToString());
            clienteProveedor.RetencionCliente1 = double.Parse(retencionCliente1.ToString());
            clienteProveedor.RetencionCliente2 = double.Parse(retencionCliente2.ToString());
            clienteProveedor.IdValorClasificacionProveedor1 = int.TryParse(idValorClasificacionProveedor1.ToString(), out var idValorClasificacionProveedor1Result) ? idValorClasificacionProveedor1Result : 0;
            clienteProveedor.IdValorClasificacionProveedor2 = int.TryParse(idValorClasificacionProveedor2.ToString(), out var idValorClasificacionProveedor2Result) ? idValorClasificacionProveedor2Result : 0;
            clienteProveedor.IdValorClasificacionProveedor3 = int.TryParse(idValorClasificacionProveedor3.ToString(), out var idValorClasificacionProveedor3Result) ? idValorClasificacionProveedor3Result : 0;
            clienteProveedor.IdValorClasificacionProveedor4 = int.TryParse(idValorClasificacionProveedor4.ToString(), out var idValorClasificacionProveedor4Result) ? idValorClasificacionProveedor4Result : 0;
            clienteProveedor.IdValorClasificacionProveedor5 = int.TryParse(idValorClasificacionProveedor5.ToString(), out var idValorClasificacionProveedor5Result) ? idValorClasificacionProveedor5Result : 0;
            clienteProveedor.IdValorClasificacionProveedor6 = int.TryParse(idValorClasificacionProveedor6.ToString(), out var idValorClasificacionProveedor6Result) ? idValorClasificacionProveedor6Result : 0;
            clienteProveedor.LimiteCreditoProveedor = double.Parse(limiteCreditoProveedor.ToString());
            clienteProveedor.DiasCreditoProveedor = int.TryParse(diasCreditoProveedor.ToString(), out var diasCreditoProveedorResult) ? diasCreditoProveedorResult : 0;
            clienteProveedor.TiempoEntrega = int.TryParse(tiempoEntrega.ToString(), out var tiempoEntregaResult) ? tiempoEntregaResult : 0;
            clienteProveedor.DiasEmbarqueProveedor = int.Parse(diasEmbarqueProveedor.ToString());
            clienteProveedor.ImpuestoProveedor1 = double.Parse(impuestoProveedor1.ToString());
            clienteProveedor.ImpuestoProveedor2 = double.Parse(impuestoProveedor2.ToString());
            clienteProveedor.ImpuestoProveedor3 = double.Parse(impuestoProveedor3.ToString());
            clienteProveedor.RetencionProveedor1 = double.Parse(retencionProveedor1.ToString());
            clienteProveedor.RetencionProveedor2 = double.Parse(retencionProveedor2.ToString());
            clienteProveedor.BanderaInteresMoratorio = int.TryParse(banderaInteresMoratorio.ToString(), out var banderaInteresMoratorioResult) ? banderaInteresMoratorioResult : 0;
            clienteProveedor.TextoExtra1 = textoExtra1.ToString();
            clienteProveedor.TextoExtra2 = textoExtra2.ToString();
            clienteProveedor.TextoExtra3 = textoExtra3.ToString();
            clienteProveedor.ImporteExtra1 = double.Parse(importeExtra1.ToString());
            clienteProveedor.ImporteExtra2 = double.Parse(importeExtra2.ToString());
            clienteProveedor.ImporteExtra3 = double.Parse(importeExtra3.ToString());
            clienteProveedor.ImporteExtra4 = double.Parse(importeExtra4.ToString());
            clienteProveedor.Id = int.Parse(id.ToString());
            clienteProveedor.Moneda = _monedaRepository.BuscarPorId(int.Parse(idMoneda.ToString()));
            clienteProveedor.NombreLargo = nombreLargo.ToString();
            clienteProveedor.Email1 = email1.ToString();
            clienteProveedor.Email2 = email2.ToString();
            clienteProveedor.Email3 = email3.ToString();
            clienteProveedor.FormaPago = formaPago.ToString();
            clienteProveedor.NumeroCuentaPago = numeroCuentaPago.ToString();
            clienteProveedor.UsoCfdi = usoCfdi.ToString();
            clienteProveedor.Almacen = _almacenRepository.BuscarPorId(clienteProveedor.IdAlmacen);
            clienteProveedor.AgenteVenta = _agenteRepository.BuscarPorId(clienteProveedor.IdAgenteVenta);
            clienteProveedor.AgenteCobro = _agenteRepository.BuscarPorId(clienteProveedor.IdAgenteCobro);
            clienteProveedor.ValorClasificacionCliente1 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionCliente1);
            clienteProveedor.ValorClasificacionCliente2 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionCliente2);
            clienteProveedor.ValorClasificacionCliente3 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionCliente3);
            clienteProveedor.ValorClasificacionCliente4 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionCliente4);
            clienteProveedor.ValorClasificacionCliente5 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionCliente5);
            clienteProveedor.ValorClasificacionCliente6 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionCliente6);
            clienteProveedor.ValorClasificacionProveedor1 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionProveedor1);
            clienteProveedor.ValorClasificacionProveedor2 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionProveedor2);
            clienteProveedor.ValorClasificacionProveedor3 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionProveedor3);
            clienteProveedor.ValorClasificacionProveedor4 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionProveedor4);
            clienteProveedor.ValorClasificacionProveedor5 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionProveedor5);
            clienteProveedor.ValorClasificacionProveedor6 = _valorClasificacionRepository.BuscarPorId(clienteProveedor.IdValorClasificacionProveedor6);

            return clienteProveedor;
        }
    }
}