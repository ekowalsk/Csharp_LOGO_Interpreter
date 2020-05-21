public interface IAST
{
    void accept(IVisitor v);
    dynamic output { get; set; }
}