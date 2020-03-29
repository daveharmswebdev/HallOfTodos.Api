using HallOfTodos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HallOfTodos.API
{
    public class TodosDataStore
    {
        public static TodosDataStore Current { get; } = new TodosDataStore();

        public List<TodosDto> Todos { get; set; }

        public TodosDataStore()
        {
            Todos = new List<TodosDto>()
            {
                new TodosDto()
                {
                    Id = new Guid("72038873-4a70-455a-afaa-6ad787633ee3"),
                    Todo = "Buy Organic Eggs from Whole Foods",
                    Doing = "Dave",
                    Description = "Test 123",
                    Complete = false,
                    Notes = new List<TodoNotesDto>()
                    {
                        new TodoNotesDto()
                        {
                            Id = new Guid("cb74c57a-88ad-4b90-9075-a7dde972c047"),
                            Title = "no eggs at Whole foods",
                            Details = "see above"
                        },
                        new TodoNotesDto()
                        {
                            Id = new Guid("a59545f8-1698-4b5d-a4e9-ca562876b351"),
                            Title = "went to kroger",
                            Details = "plenty of eggs here"
                        },
                    }
                },
                new TodosDto()
                {
                    Id = new Guid("7be07a44-1b2b-4d5a-ad96-2ecc3699ae90"),
                    Todo = "Buy cat food",
                    Doing = "Dave",
                    Description = "Picanha boudin meatloaf turducken ribeye ham hock, chuck flank doner tri-tip swine. T-bone brisket prosciutto buffalo tenderloin. Kielbasa kevin cow, shank meatball beef doner. Turkey picanha beef ribs, bresaola prosciutto shoulder buffalo cupim alcatra drumstick shank pork belly ham capicola. Jerky brisket shankle tail landjaeger. Boudin spare ribs salami t-bone, bresaola pastrami filet mignon pig ham. Pork belly flank shankle tongue cow strip steak picanha short ribs shoulder turkey rump burgdoggen pork frankfurter.",
                    Complete = false,
                    Notes = new List<TodoNotesDto>()
                    {
                        new TodoNotesDto()
                        {
                            Id = new Guid("06d9ba77-0ba3-4395-b618-61cde7e1d94c"),
                            Title = "got cat food at pet smart",
                            Details = "see above"
                        },
                        new TodoNotesDto()
                        {
                            Id = new Guid("85c72ac1-516c-40cd-858d-11e6c6982ae3"),
                            Title = "30 dollars a bag",
                            Details = "plenty of eggs here"
                        },
                    }
                },
                new TodosDto()
                {
                    Id = new Guid("c76cdf90-2beb-41d8-99c6-c4d0f0e783d9"),
                    Todo = "take suit to cleaners",
                    Doing = "Dave",
                    Description = "Test 123",
                    Complete = false,
                    Notes = new List<TodoNotesDto>()
                    {
                        new TodoNotesDto()
                        {
                            Id = new Guid("977cad2e-94b7-49c7-85b3-6aab233d7e03"),
                            Title = "used heritage cleaners",
                            Details = "see above"
                        }
                    }
                },
                new TodosDto()
                {
                    Id = new Guid("c76cdf90-2beb-41d8-99c6-c4d0f0e783d9"),
                    Todo = "yeah",
                    Doing = "Dave",
                    Description = "Test 123",
                    Complete = false,
                    Notes = new List<TodoNotesDto>()
                    {
                        new TodoNotesDto()
                        {
                            Id = new Guid("082e2722-f38d-4563-8289-94c567685d9a"),
                            Title = "3 dollars a bag",
                            Details = "plenty of chips here"
                        },
                    }
                },

            };
        }
    }
}
