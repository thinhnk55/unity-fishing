using Framework;
using System.Collections.Generic;

public class AnswerCollection : CollectionViewBase<AnswerInfo>
{
    public override void BuildView()
    {
    }

    public override void BuildView(List<AnswerInfo> infos)
    {
        base.BuildView(infos);
    }
}
