using LibraryLending.Application.Features.Lending.BorrowBookCopy;
using LibraryLending.Application.Features.Lending.ReturnBookCopy;

namespace LibraryLending.Testing.TestData;


public static class CommandFactory
{
    public static BorrowBookCopyCommand BorrowBookCopy(
        string memberNumber = "M-0001",
        string copyBarcode = "BC-0001")
        => new(memberNumber, copyBarcode);

    public static ReturnBookCopyCommand ReturnBookCopy(
        string copyBarcode = "BC-0001")
        => new(copyBarcode);
}
