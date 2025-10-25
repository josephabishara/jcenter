using JeCenterWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace JeCenterWeb.Models.Repository
{

    public class CardsRepository
    {
        private readonly ApplicationDbContext _context;
        public CardsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCardwrite(int val, int cnt)
        {
            Cardwrite cardwrite = new Cardwrite();
            cardwrite.Cardvalue = val;
            cardwrite.CardCount = cnt;
            _context.Add(cardwrite);
            await _context.SaveChangesAsync();
            await createcards(val, cnt);
        }
        public async Task createcards(int val, int cnt)
        {
            string code;
            for (int i = 0; i < cnt; i++)
            {
                code = RandomPassword(14);
                ChargingCard? old = await _context.ChargingCard
                    .Where(c => c.CardCode == code)
                    .FirstOrDefaultAsync();
                if (old != null)
                {
                    i--;
                }
                else
                {
                    ChargingCard chargingCard = new ChargingCard
                    {
                        CardCode = code,
                        CardValue = val,
                        State = false,
                     
                        StudentID = 0,
                    };
                    _context.Add(chargingCard);
                    await _context.SaveChangesAsync();
                }

            }
        }

        public static string RandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyz";

            Random randNum = new Random();

            char[] chars = new char[PasswordLength];

            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }
    }
}
