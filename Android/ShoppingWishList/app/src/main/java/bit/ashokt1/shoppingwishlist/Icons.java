package bit.ashokt1.shoppingwishlist;

import android.graphics.drawable.Drawable;

public class Icons
{
    Drawable iconImage;
    String iconName;
    String iconDescription;

    public Icons (Drawable iconImage, String iconName, String iconDescription)
    {
        this.iconName = iconName;
        this.iconDescription = iconDescription;
        this.iconImage = iconImage;
    }

    @Override
    public String toString()
    {
        return iconName + "\n" + "\n" +iconDescription;
    }
}
