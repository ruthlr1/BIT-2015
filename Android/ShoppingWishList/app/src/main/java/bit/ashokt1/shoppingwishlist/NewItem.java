package bit.ashokt1.shoppingwishlist;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.media.ExifInterface;
import android.net.Uri;
import android.os.Environment;
import android.provider.MediaStore;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.view.WindowManager;
import android.view.inputmethod.EditorInfo;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class NewItem extends Activity {

    private String photoFileName;
    private File photoFile;
    private Uri photoFileUri;
    private final int REQUEST_CODE_CAMERA = 1;
    private TextView titleText;
    private TextView priceText;
    private TextView storeText;
    private ImageView takenImage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_item);

        //Start Camera Button
        ImageView startCamera = (ImageView) findViewById(R.id.imageViewStartCamera);
        startCamera.setOnClickListener(new PIntent());

        //Save Button
        ImageView saveBtn = (ImageView) findViewById(R.id.imageViewSave);
        saveBtn.setOnClickListener(new InsertIntoTheDataBase());

        this.getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
    }

    public class PIntent implements View.OnClickListener {

        @Override
        public void onClick(View view) {
            //creates time stamped file to hold the image data
            try {
                photoFile = createTimeStampedFile();

                //Generate URI from the file instance
                photoFileUri = Uri.fromFile(photoFile);

                //create an intent for the image capture content provider
                Intent imageCaptureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);

                //attach your URI to the intent
                imageCaptureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoFileUri);

                //launch intent waiting for the result
                startActivityForResult(imageCaptureIntent, REQUEST_CODE_CAMERA);
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }
    }

    public class InsertIntoTheDataBase implements View.OnClickListener {

        @Override
        public void onClick(View view) {
            SimpleDateFormat timeStampFormat = new SimpleDateFormat("EEE dd MMM yyyy HH:mm");
            Date time = new Date();
            String timeStamp = timeStampFormat.format(time);

            titleText = (TextView) findViewById(R.id.editTitleText);
            priceText = (TextView) findViewById(R.id.editPriceText);
            storeText = (TextView) findViewById(R.id.editStoreText);
            takenImage = (ImageView) findViewById(R.id.imageViewPhoto);

            titleText.setImeOptions(EditorInfo.IME_ACTION_DONE);
            storeText.setImeOptions(EditorInfo.IME_ACTION_DONE);

            if (takenImage.getDrawable() == null) {
                Toast.makeText(NewItem.this, "Please take a photo", Toast.LENGTH_LONG).show();
            } else {
                // Checks if either field is empty
                if ((titleText.getText().toString().equals("")) || (priceText.getText().toString().equals("")) || (storeText.getText().toString().equals(""))) {
                    Toast.makeText(NewItem.this, "Please complete all the fields", Toast.LENGTH_LONG).show();
                } else {

                    // converts the number for price to a double
                    double convertedPriceFromStringToDouble = Double.parseDouble(priceText.getText().toString());

                    // converts the double to a string with 2 significant figures
                    String formatPriceBackToString = String.format("%.2f", convertedPriceFromStringToDouble);

                    String photoPath = photoFile.getPath();
                    // Insert into database
                    String query = "INSERT INTO tblPictures (itemName, picture, price, store, date) VALUES ('" + titleText.getText() + "', '" + photoPath + "','" + formatPriceBackToString + "','" + storeText.getText() + "','" +
                            timeStamp + "' );";

                    Intent newActivity = new Intent(NewItem.this, AllItems.class);

                    newActivity.putExtra("Query String", query);
                    startActivity(newActivity);
                }
            }
        }
    }

    public File createTimeStampedFile() {
        //fetch system image folder
        File imageRootPath = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_PICTURES);

        //make subdirectory
        File imageStorageDirectory = new File(imageRootPath, "CameraList");

        if (!imageStorageDirectory.exists()) {
            imageStorageDirectory.mkdirs(); //mkdirs creates parent directories as required
        }
        //get timestamp
        SimpleDateFormat timeStampFormat = new SimpleDateFormat("yyyyMMdd_HHmmss");
        Date currentTime = new Date();
        String timeStamp = timeStampFormat.format(currentTime);

        //make filename
        photoFileName = "IMG_" + timeStamp + ".jpg";

        //make file object from directory and filename
        File newImageFile = new File(imageStorageDirectory.getPath() + File.separator + photoFileName);
        return newImageFile;
    }

    //------------------------------------------------------------------------------------------------------------
    //              AFTER THE PHOTO HAS BEEN TAKEN
    //------------------------------------------------------------------------------------------------------------

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        //return we are looking for?
        if (requestCode == REQUEST_CODE_CAMERA) {
            //get data?
            if (resultCode == RESULT_OK) {

                //need the filepath for bitmap factory, not the file
                String realFilePath = photoFile.getPath();

                int rotate = getRotation(realFilePath); //calls the getRotation method and passes in the name of the photo file path

                Matrix matrix = new Matrix();
                matrix.postRotate(rotate);

                Bitmap userPhotoBitMap = BitmapFactory.decodeFile(realFilePath);
                Bitmap rotatedBitmap = Bitmap.createBitmap(userPhotoBitMap, 0, 0, userPhotoBitMap.getWidth(), userPhotoBitMap.getHeight(), matrix, true);

                // Display the bitmap in each ImageView
                ImageView ul = (ImageView) findViewById(R.id.imageViewPhoto);
                ul.setImageBitmap(rotatedBitmap);

            } else {
                Toast.makeText(NewItem.this, "No photo saved, something is wrong", Toast.LENGTH_LONG).show();
            }
        }
    }

    public int getRotation(String imgPath) {
        int rotate = 0;

        try
        {
            ExifInterface exIf = new ExifInterface(imgPath);
            String rotationAmount = exIf.getAttribute(ExifInterface.TAG_ORIENTATION);

            if (!TextUtils.isEmpty(rotationAmount))
            {
                int rotationParameter = Integer.parseInt(rotationAmount); //rotation amount gets converted to an integer
                switch(rotationParameter)
                {
                    case ExifInterface.ORIENTATION_NORMAL:
                        rotate=0;
                        break;

                    case ExifInterface.ORIENTATION_ROTATE_90:
                        rotate=90;
                        break;

                    case ExifInterface.ORIENTATION_ROTATE_180:
                        rotate=180;
                        break;

                    case ExifInterface.ORIENTATION_ROTATE_270:
                        rotate=270;
                        break;
                }

            }

        }
       catch (IOException e)
       {
            e.printStackTrace();
       }
        return rotate;
    }
}


