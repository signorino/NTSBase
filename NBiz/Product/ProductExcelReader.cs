﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NModel;
using System.Data;
using System.Text.RegularExpressions;
using NLibrary;
namespace NBiz
{


    public class ProductExcelReader : IExcelReader<Product>
    {
        BLLBase<Supplier> bllSupplier = new BLLBase<Supplier>();
       // SerialNumberManager snm = new SerialNumberManager();
        StringBuilder sbReadMsg = new StringBuilder();
        public IList<Product> Read(System.IO.Stream stream) {
            IList allPicture;
            return Read(stream, out allPicture);
        }
        public IList<Product> Read(System.IO.Stream stream,out IList allPictures)
        {
            DataTable dt = new TransferInDatatable().CreateFromXsl(stream,false,out allPictures);

            IRowPopulate irp = RowPopulateFactory.CreatePopulator(dt);
            foreach (DataColumn col in dt.Columns)
            {
                ColumnNameMatch(dt, col.ColumnName);
            }
            List<Product> productList = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                Product p = irp.PopulateFromRow(row);
                productList.Add(p);
            }
            
            return productList;
        }

     
        
        /// <summary>
        /// 列名容错
        /// </summary>
        private void ColumnNameMatch(DataTable dt, string columnName)
        {
            Dictionary<string, string> columnsEasyToSpellWrong = new Dictionary<string, string>();
            columnsEasyToSpellWrong.Add("生产周期", ".*生产周期.*");
            columnsEasyToSpellWrong.Add("最小起订量", ".*最小起订量.*");
            columnsEasyToSpellWrong.Add("规格参数", ".*规格.*参数.*");
            columnsEasyToSpellWrong.Add("产地", "产地|开票地");
            //英文列名匹配
          //  columnsEasyToSpellWrong.Add("生产周期", ".*生产周期.*");
            // {"*生产周期*","*最小起定量*" };
            foreach (KeyValuePair<string, string> columnNamePatern in columnsEasyToSpellWrong)
            {
                if (Regex.IsMatch(columnName, columnNamePatern.Value))
                {
                    dt.Columns[columnName].ColumnName = columnNamePatern.Key;
                }
            }
        }
    }
}
