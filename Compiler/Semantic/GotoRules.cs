using Lucene.Net.Support;
using System.Linq;

namespace Compiler.Semantic
{
    public class GotoRules
    {
        public List<KeyValuePair<string, string>> Rules { get; private set; }

        public GotoRules()
        {
            Rules = new List<KeyValuePair<string, string>>()
           {
                new KeyValuePair<string, string>("<variable>", "<comma>"),
                new KeyValuePair<string, string>("<variable>", "<colon>"),
                new KeyValuePair<string, string>("<variable>", "<assign>"),
                new KeyValuePair<string, string>("<variable>", "<semicolon>"),
                new KeyValuePair<string, string>("<variable>", "<unary operator NOT>")                       ,
                new KeyValuePair<string, string>("<variable>", "<binary operator IMP>")                      ,
                new KeyValuePair<string, string>("<variable>", "<binary operator AND>")                      ,
                new KeyValuePair<string, string>("<variable>", "<binary operator OR>")                       ,
                new KeyValuePair<string, string>("<variable>", "<closing bracket>")                          ,
                new KeyValuePair<string, string>("<start of calculation part>", "<variable>")                ,
                new KeyValuePair<string, string>("<start of calculation part>", "<end of calculation part>") ,
                new KeyValuePair<string, string>("<start of calculation part>", "<\"while\" header>")        ,
                new KeyValuePair<string, string>("<start of calculation part>", "<function name>")           ,
                new KeyValuePair<string, string>("<variables declaration>", "<variable>")                    ,
                new KeyValuePair<string, string>("<variables type>", "<semicolon>")                          ,
                new KeyValuePair<string, string>("<binary operator AND>", "<variable>")                      ,
                new KeyValuePair<string, string>("<binary operator AND>", "<constant>")                      ,
                new KeyValuePair<string, string>("<binary operator AND>", "<opening bracket>"),
                new KeyValuePair<string, string>("<binary operator OR>", "<variable>")                       ,
                new KeyValuePair<string, string>("<binary operator OR>", "<constant>")                       ,
                new KeyValuePair<string, string>("<\"while\" header>", "<opening bracket>")                  ,
                new KeyValuePair<string, string>("<start of \"while\" block>", "<variable>")                 ,
                new KeyValuePair<string, string>("<start of \"while\" block>", "<\"while\" header>")         ,
                new KeyValuePair<string, string>("<start of \"while\" block>", "<function name>")            ,
                new KeyValuePair<string, string>("<end of \"while\" block>", "<variable>")                   ,
                new KeyValuePair<string, string>("<end of \"while\" block>", "<end of \"while\" block>")     ,
                new KeyValuePair<string, string>("<end of \"while\" block>", "<end of calculation part>")    ,
                new KeyValuePair<string, string>("<end of \"while\" block>", "<\"while\" header>")           ,
                new KeyValuePair<string, string>("<end of \"while\" block>", "<function name>")              ,
                new KeyValuePair<string, string>("<unary operator NOT>", "<variable>")                       ,
                new KeyValuePair<string, string>("<unary operator NOT>", "<constant>")                       ,
                new KeyValuePair<string, string>("<unary operator NOT>", "<opening bracket>")                ,
                new KeyValuePair<string, string>("<binary operator IMP>", "<variable>")                      ,
                new KeyValuePair<string, string>("<binary operator IMP>", "<constant>")                      ,
                new KeyValuePair<string, string>("<binary operator IMP>", "<opening bracket>")               ,
                new KeyValuePair<string, string>("<function name>", "<opening bracket>")                     ,
                new KeyValuePair<string, string>("<colon>", "<variables type>")                              ,
                new KeyValuePair<string, string>("<semicolon>", "<start of calculation part>")               ,
                new KeyValuePair<string, string>("<semicolon>", "<end of calculation part>")                 ,
                new KeyValuePair<string, string>("<semicolon>", "<end of \"while\" block>")                  ,
                new KeyValuePair<string, string>("<semicolon>", "<variable>")                                ,
                new KeyValuePair<string, string>("<semicolon>", "<function name>")                           ,
                new KeyValuePair<string, string>("<semicolon>", "<\"while\" header>")                        ,
                new KeyValuePair<string, string>("<comma>", "<variable>")                                    ,
                new KeyValuePair<string, string>("<assign>", "<variable>")                                   ,
                new KeyValuePair<string, string>("<assign>", "<constant>")                                   ,
                new KeyValuePair<string, string>("<assign>", "<unary operator NOT>")                         ,
                new KeyValuePair<string, string>("<assign>", "<binary operator IMP>")                        ,
                new KeyValuePair<string, string>("<assign>", "<opening bracket>")                            ,
                new KeyValuePair<string, string>("<opening bracket>", "<opening bracket>")                   ,
                new KeyValuePair<string, string>("<opening bracket>", "<variable>")                          ,
                new KeyValuePair<string, string>("<opening bracket>", "<constant>")                          ,
                new KeyValuePair<string, string>("<opening bracket>", "<unary operator NOT>")                ,
                new KeyValuePair<string, string>("<opening bracket>", "<binary operator IMP>")               ,
                new KeyValuePair<string, string>("<closing bracket>", "<closing bracket>")                   ,
                new KeyValuePair<string, string>("<closing bracket>", "<semicolon>")                         ,
                new KeyValuePair<string, string>("<closing bracket>", "<unary operator NOT>")                ,
                new KeyValuePair<string, string>("<closing bracket>", "<binary operator IMP>")               ,
                new KeyValuePair<string, string>("<closing bracket>", "<binary operator AND>")               ,
                new KeyValuePair<string, string>("<closing bracket>", "<binary operator OR>")                ,
                new KeyValuePair<string, string>("<closing bracket>", "<start of \"while\" block>"),             
                new KeyValuePair<string, string>("<constant>", "<binary operator IMP>")                      ,
                new KeyValuePair<string, string>("<constant>", "<unary operator NOT>")                       ,
                new KeyValuePair<string, string>("<constant>", "<binary operator AND>")                      ,
                new KeyValuePair<string, string>("<constant>", "<binary operator OR>")                       ,
                new KeyValuePair<string, string>("<constant>", "<closing bracket>")                          ,
                new KeyValuePair<string, string>("<constant>", "<semicolon>")
           };
        }
        public bool CheckRule(Identifier lexem1, Identifier lexem2)
        {
            if (Rules.Where(e => e.Key == lexem1.Type && e.Value == lexem2.Type).Any())
                return true;
            return false;
        }

    }
}
