using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMStockManageSystem
{
    internal class Product
    {
        // 정보은닉 위해서 getter setter 사용하기 (예시로 productID에 대한 getter와 setter를 작성)

        public string productID { get; set; }// 상품 식별번호

        public string productName { get; set; } // 상품 이름

        public int stockQuantity { get; set; } // 재고 수량

        public int price { get; set; } // 상품 가격

        public string exp { get; set; }   //유통기한

        public string category { get; set; } // 상품 카테고리 (예: 식품, 음료, 생활용품 등)




    }
}
