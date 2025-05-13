using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class CategoryBL
    {
        private CategoryDL categoryDL;

        public CategoryBL()
        {
            categoryDL = new CategoryDL();
        }

        public void Add(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.CatName) || string.IsNullOrEmpty(category.CatDesc))
                throw new Exception("Tên và mô tả danh mục không được để trống");
            categoryDL.Add(category);
        }

        public void Update(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.CatName) || string.IsNullOrEmpty(category.CatDesc))
                throw new Exception("Tên và mô tả danh mục không được để trống");
            categoryDL.Update(category);
        }

        public void Delete(int id)
        {
            categoryDL.Delete(id);
        }

        public DataTable GetAll()
        {
            return categoryDL.GetAll();
        }
    }
}
