using Esculturas.Core.Configuration;
using Esculturas.Core.Data.Interfaces;
using Esculturas.Core.Entities;
using Esculturas.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Esculturas.Core.Data
{
    public class EsculturaRepository : IEsculturaRepository
    {
        private ICurrentConfiguration _currentConfiguration { get; set; }

        public EsculturaRepository(ICurrentConfiguration currentConfiguration)
        {
            _currentConfiguration = currentConfiguration;
        }

        public List<Escultura> GetListTest()
        {
            var list = new List<Escultura>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(new Escultura()
                {
                    Nombre = $"Escultura Data{i}",
                    Coordenada = new Coordenada(latitude: -58.98178609950485, longitude: -27.44692119526704)
                });
            }

            return list;
        }

        public List<Escultura> GetList() {

            var list = GetDataFromExcel(_currentConfiguration.DataFilePathComplete);

            return list;
        }

        private List<Escultura> GetDataFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }

                var ws = pck.Workbook.Worksheets.First();

                var esculturas = new List<Escultura>();

                //foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                //{
                //    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format(“Column { 0}”, firstRowCell.Start.Column));
                //}
                
                var startRow = hasHeader ? 2 : 1;

                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var rowData = (object[,])wsRow.Value;

                    var escultura = new Escultura() {
                        Numero = Convert.ToInt32(rowData[0, (int)EsculturaExcelColumEnum.Numero]),
                        Nombre = DataToString(rowData, EsculturaExcelColumEnum.Nombre),
                        Escultor = new Escultor(DataToString(rowData, EsculturaExcelColumEnum.Escultor)),
                        Material = DataToString(rowData, EsculturaExcelColumEnum.Material),
                        Direccion = DataToString(rowData, EsculturaExcelColumEnum.Direccion),
                        Coordenada = new Coordenada(DataToString(rowData, EsculturaExcelColumEnum.Coordenada)),
                        Premios = DataToString(rowData, EsculturaExcelColumEnum.Premios)
                    };

                    esculturas.Add(escultura);
                }
                return esculturas;
            }
        }

        private string DataToString(object[,] rowData, EsculturaExcelColumEnum column) {
            var value = rowData[0, (int)column];
            if (value != null)
            {
                return value.ToString();
            }

            return string.Empty;
        }
    }
}
