using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Pokemon_Project
{
  class Menu
  {
    // 상점이면 아이템구입, 아이템판매 등등
    // 전투면 전투, 아이템사용, 도망
    // 전투면 무슨 스킬 선택할지
    // 키입력 감지해서 무슨 메뉴를 선택했는지 판단

    // 타이틀 메뉴
    enum MainMenu
    {
      게임시작,
      게임종료
    }
    // 상점 메뉴
    enum ShopMenu
    {
      아이템구매,
      아이템판매
    }

  }
}
