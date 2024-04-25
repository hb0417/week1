using System;
using System.Collections.Generic;

namespace camp_1week
{
    class RPGGame
    {
        class Character
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Level { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public void AddEquippedItemAttack(Item item)
            {
                Attack += item.Attack;
            }

            // 장착한 아이템의 방어력을 추가하는 메서드
            public void AddEquippedItemDefense(Item item)
            {
                Defense += item.Defense;
            }

            public int Health { get; set; }
            public int Gold { get; set; }
            public List<Item> Inventory { get; set; }

            public Character()
            {
                Inventory = new List<Item>();
            }
        }

        class Item
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int Attack { get; set; } // 추가: 공격력
            public int Defense { get; set; } // 추가: 방어력
            public bool Equipped { get; set; }
            public bool Purchased { get; set; }

            public Item()
            {
                Equipped = false; // 아이템이 장착되지 않은 상태로 초기화
                Purchased = false; // 아이템이 구매되지 않은 상태로 초기화
            }
        }

        static Character player = new Character
        {
            Name = "Chad",
            Class = "전사",
            Level = 1,
            Attack = 10,
            Defense = 5,
            Health = 100,
            Gold = 1500
        };

        static Item[] shopItems = new Item[]
        {
            new Item { Name = "수련자 갑옷", Description = "방어력 +5 | 수련에 도움을 주는 갑옷입니다.", Price = 1000 },
            new Item { Name = "무쇠갑옷", Description = "방어력 +9 | 무쇠로 만들어져 튼튼한 갑옷입니다.", Price = 1200 },
            new Item { Name = "스파르타의 갑옷", Description = "방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", Price = 3500 },
            new Item { Name = "낡은 검", Description = "공격력 +2 | 쉽게 볼 수 있는 낡은 검 입니다.", Price = 600 },
            new Item { Name = "청동 도끼", Description = "공격력 +5 | 어디선가 사용됐던거 같은 도끼입니다.", Price = 1500 },
            new Item { Name = "스파르타의 창", Description = "공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다.", Price = 5000 }
        };

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분을 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine();
                Console.Write("원하시는 행동을 입력해주세요: ");

                int choice = GetChoice(1, 3);

                switch (choice)
                {
                    case 1:
                        ViewStatus();
                        break;
                    case 2:
                        OpenInventory();
                        break;
                    case 3:
                        VisitShop();
                        break;
                }
            }
        }

        static void ViewStatus()
        {
            // 상태 보기 기능 구현
            Console.WriteLine("상태 보기");

            // 캐릭터 정보 출력
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Class} )");
            Console.WriteLine($"공격력: {player.Attack}");
            Console.WriteLine($"방어력: {player.Defense}");
            Console.WriteLine($"체력: {player.Health}");
            Console.WriteLine($"Gold: {player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("0. 나가기");

            // 사용자 입력 처리
            Console.Write("원하시는 행동을 입력해주세요: ");
            int choice = GetChoice(0, 0); // 나가기 옵션만 있으므로 0만 허용
            Console.WriteLine();
        }

        static void OpenInventory()
        {
            // 인벤토리 기능 구현
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                string equippedMarker = player.Inventory[i].Purchased ? "구매완료" : $"{player.Inventory[i].Price} G";
                string equipped = player.Inventory[i].Equipped ? "[E] " : "";
                Console.WriteLine($"- {i + 1} {equipped}{player.Inventory[i].Name} | {player.Inventory[i].Description} | {equippedMarker}");
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요: ");

            int choice = GetChoice(0, 1);

            if (choice == 1)
            {
                ManageEquipment();
            }
        }

        static void ManageEquipment()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                string equipped = player.Inventory[i].Equipped ? "[E] " : "";
                Console.WriteLine($"- {i + 1} {equipped}{player.Inventory[i].Name} | {player.Inventory[i].Description}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요: ");

            int choice = GetChoice(0, player.Inventory.Count);

            if (choice != 0)
            {
                // 선택한 아이템의 인덱스
                int itemIndex = choice - 1;

                // 아이템을 장착하거나 장착 해제
                Item selectedItem = player.Inventory[itemIndex];
                selectedItem.Equipped = !selectedItem.Equipped;

                // 장착한 아이템의 효과를 적용
                if (selectedItem.Equipped)
                {
                    player.AddEquippedItemAttack(selectedItem);
                    player.AddEquippedItemDefense(selectedItem);
                    Console.WriteLine($"{selectedItem.Name}을(를) 장착했습니다.");
                }
                else
                {
                    player.Attack -= selectedItem.Attack;
                    player.Defense -= selectedItem.Defense;
                    Console.WriteLine($"{selectedItem.Name}의 장착을 해제했습니다.");
                }

                // 사용자 입력 대기
                Console.WriteLine("아무 키나 눌러주세요...");
                Console.ReadKey(true);
                Console.WriteLine();
            }
        }

        static void VisitShop()
        {
            // 상점 기능 구현
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            // 보유 골드 표시
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            // 아이템 목록 표시
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Length; i++)
            {
                string purchaseStatus = shopItems[i].Purchased ? "구매완료" : $"{shopItems[i].Price} G";
                Console.WriteLine($"- {i + 1} {shopItems[i].Name} | {shopItems[i].Description} | {purchaseStatus}");
            }
            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.Write("원하시는 행동을 입력해주세요: ");
            int choice = GetChoice(0, 1);

            if (choice == 1)
            {
                BuyItem();
            }
        }

        static void BuyItem()
        {
            Console.Write("구매할 아이템 번호를 입력하세요: ");
            int itemNumber = GetChoice(1, shopItems.Length) - 1;

            Item selectedItem = shopItems[itemNumber];

            // 이미 구매한 아이템인지 확인
            if (selectedItem.Purchased)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                Console.WriteLine("아무 키나 눌러주세요...");
                Console.ReadKey(true);
                Console.WriteLine();
                return;
            }

            // 가격 확인 및 구매 처리
            if (player.Gold >= selectedItem.Price)
            {
                player.Gold -= selectedItem.Price;
                selectedItem.Purchased = true; // 구매 상태로 변경
                Console.WriteLine("구매를 완료했습니다.");

                // 인벤토리에 아이템 추가
                player.Inventory.Add(selectedItem);

                // 아이템 구매로 인한 상태 변경 적용
                ApplyItemEffects(selectedItem);
            }
            else
            {
                Console.WriteLine("보유한 골드가 부족합니다.");
            }

            // 사용자 입력 대기
            Console.WriteLine("아무 키나 눌러주세요...");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        static void ApplyItemEffects(Item item)
        {
            // 아이템 효과 적용
            player.Attack += item.Attack;
            player.Defense += item.Defense;
        }

        static int GetChoice(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                Console.Write("원하시는 행동을 입력해주세요: ");
            }
            return choice;
        }
    }
}
