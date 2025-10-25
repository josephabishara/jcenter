using JeCenterWeb.Data;
using Microsoft.AspNetCore.Identity;

namespace JeCenterWeb.Models.Repository
{
    public class FinancialDocumentEntryRepository
    {
        // FinancialJournalEntryRepository

        protected ApplicationDbContext _context;
        public FinancialDocumentEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateDocument(FinancialDocuments financialDocuments)
        {
            _context.Add(financialDocuments);
            await _context.SaveChangesAsync();
            await createFinancialJournalEntryLine(financialDocuments);

            return 1;

        }
        public async Task createFinancialJournalEntryLine(FinancialDocuments financialDocuments)
        {

            FinancialJournalEntryLine journalEntryLine1 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
            };

            if (financialDocuments.MovementTypeId == 2)
            {
                journalEntryLine1.AccountID = financialDocuments.AccountID;
                journalEntryLine1.Debit = financialDocuments.Value;
                journalEntryLine1.Credit = 0;
            }
            else if (financialDocuments.MovementTypeId == 3)
            {
                journalEntryLine1.AccountID = financialDocuments.TreasuryID;
                journalEntryLine1.Debit = 0;
                journalEntryLine1.Credit = financialDocuments.Value;
            }

            _context.Add(journalEntryLine1);
            await _context.SaveChangesAsync();

            FinancialJournalEntryLine journalEntryLine2 = new FinancialJournalEntryLine()
            {
                FinancialDocumentId = financialDocuments.FinancialDocumentId,
                physicalyearId = financialDocuments.physicalyearId,
                JournalEntryDate = financialDocuments.JournalEntryDate,
            };

            if (financialDocuments.MovementTypeId == 2)
            {
                journalEntryLine2.AccountID = financialDocuments.TreasuryID;
                journalEntryLine2.Debit = 0;
                journalEntryLine2.Credit = financialDocuments.Value;
            }
            else if (financialDocuments.MovementTypeId == 3)
            {
                journalEntryLine2.AccountID = financialDocuments.AccountID;
                journalEntryLine2.Debit = financialDocuments.Value;
                journalEntryLine2.Credit = 0;
            }

            _context.Add(journalEntryLine2);
            await _context.SaveChangesAsync();

        }

    }
}
