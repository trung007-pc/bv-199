namespace Core.Const
{
    public  class ContentRegularExpression
    {
        public const string NAME =
            @"^\w[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂẾưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\s\W]+$";

        public const string EMAIL = @"^\w+@gmail.com$";
        public const string NUMBER_PHONE = @"^\d{10}$";
        public const string USER_NAME = @"^\w+$";
        public const string PASSWORD = @"^\w+$";
    }
}