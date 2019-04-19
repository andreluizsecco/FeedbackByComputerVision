using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using OpenCvSharp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoAnalysisApp
{
    public partial class Main : Form
    {
        private FaceClient _faceClient = null;
        private string _subscriptionKey = "YourSubscriptionKey";
        private string _endpoint = "https://brazilsouth.api.cognitive.microsoft.com";
        private Mat _videoFrame = null;

        private Pen _pen;
        private List<DetectedFace> _detectedFaces;
        private Dictionary<string, Bitmap> _advertisingList;
        private KeyValuePair<string, Bitmap> _currentadvertising;
        private Random _random;

        public Main()
        {
            InitializeComponent();

            _pen = new Pen(Color.Red, 4);
            _detectedFaces = new List<DetectedFace>();
            _random = new Random(0);

            _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(_subscriptionKey))
            {
                Endpoint = _endpoint
            };
            _advertisingList = new Dictionary<string, Bitmap>();
            _advertisingList.Add("Jardinagem", Properties.Resources.propaganda_jardinagem);
            _advertisingList.Add("Mecânica", Properties.Resources.propaganda_mecanica);
            _advertisingList.Add("Mercado", Properties.Resources.propaganda_mercado);
            _advertisingList.Add("Pet Shop", Properties.Resources.propaganda_petshop);
            _advertisingList.Add("Salão de Beleza", Properties.Resources.propaganda_salaoBeleza);
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            if (videoRadioButton.Checked)
            {
                if (!string.IsNullOrEmpty(VideoPathTextBox.Text))
                    backgroundWorker_Video.RunWorkerAsync();
                else
                {
                    MessageBox.Show("Selecione um vídeo!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (cameraRadioButton.Checked)
                backgroundWorker_Camera.RunWorkerAsync();

            if (AnalyzeTypeComboBox.SelectedIndex == 1)
            {
                AdvertisingPictureBox.Visible = true;
                ChangeCurrentAdvertising();
            }
            else
                AdvertisingPictureBox.Visible = false;

            timer.Interval = (int)detectionIntervalNumericUpDown.Value * 1000;
            timer.Start();

            StartButton.Enabled = false;
            panel.Enabled = false;
        }

        private void StopButton_Click(object sender, System.EventArgs e)
        {
            if (backgroundWorker_Video != null)
            {
                backgroundWorker_Video.CancelAsync();
                backgroundWorker_Video.Dispose();
            }
            if (backgroundWorker_Camera != null)
            {
                backgroundWorker_Camera.CancelAsync();
                backgroundWorker_Camera.Dispose();
            }
            timer.Stop();
            StartButton.Enabled = true;
            panel.Enabled = true;
        }

        private void ChangeCurrentAdvertising()
        {
            _currentadvertising = GetRandomAdvertising();
            AdvertisingPictureBox.Image = _currentadvertising.Value;
        }

        private void AnalyzeTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var analyzeTypeAdvertising = AnalyzeTypeComboBox.SelectedIndex == 1;
            cameraRadioButton.Checked = true;
            videoRadioButton.Enabled = !analyzeTypeAdvertising;
            cameraRadioButton.Enabled = !analyzeTypeAdvertising;
        }

        private void backgroundWorker_Video_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var videoCapture = new VideoCapture(VideoPathTextBox.Text))
            {
                var interval = (int)(1000 / videoCapture.Fps);
                using (var image = new Mat())
                {
                    while (backgroundWorker_Video != null && !backgroundWorker_Video.CancellationPending)
                    {
                        videoCapture.Read(image);
                        if (image.Empty())
                            break;

                        backgroundWorker_Video.ReportProgress(0, image);
                        Thread.Sleep(interval);
                    }
                }
            }
        }

        private void backgroundWorker_Camera_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var capture = new VideoCapture(CaptureDevice.Any, index: 0))
            {
                var fps = 30;
                capture.Fps = fps;
                var interval = (int)(1000 / fps);

                using (var image = new Mat())
                {
                    while (backgroundWorker_Camera != null && !backgroundWorker_Camera.CancellationPending)
                    {
                        capture.Read(image);
                        if (image.Empty())
                            break;

                        backgroundWorker_Camera.ReportProgress(0, image);
                        Thread.Sleep(interval);
                    }
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var image = e.UserState as Mat;
            if (image == null) return;

            pictureBox.RefreshIplImage(image);
            _videoFrame = image;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker_Video.Dispose();
            backgroundWorker_Camera.Dispose();
            StartButton.Enabled = true;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            VideoPathTextBox.Text = openFileDialog.FileName;
        }

        private async Task DetectFaces(MemoryStream image, int analyzeType)
        {
            var attrs = new List<FaceAttributeType> {
                FaceAttributeType.Age,
                FaceAttributeType.Gender,
                FaceAttributeType.Emotion,
                FaceAttributeType.Makeup,
                FaceAttributeType.Hair,
                FaceAttributeType.FacialHair,
                FaceAttributeType.Glasses
            };
            var faces = await _faceClient.Face.DetectWithStreamWithHttpMessagesAsync(image, returnFaceAttributes: attrs);
            _detectedFaces.Clear();

            foreach (var face in faces.Body)
            {
                _detectedFaces.Add(face);
                Task.Run(() =>
                {
                    if (analyzeType == 0)
                        SaveFaceData(face);
                    else if (analyzeType == 1)
                    {
                        SaveAdvertisingFaceData(face, _currentadvertising.Key);
                        ChangeCurrentAdvertising();
                    }
                });
            }
        }

        private KeyValuePair<string, Bitmap> GetRandomAdvertising()
        {
            var index = _random.Next(5);
            var nextAdvertising = _advertisingList.ElementAt(index);

            if (nextAdvertising.Key != _currentadvertising.Key && 
                GetAdvertisingFacePredominant(_detectedFaces.FirstOrDefault(), nextAdvertising.Key) != "Anger")
                return nextAdvertising;
            else
                return GetRandomAdvertising();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (_videoFrame != null)
            {
                var image = new Mat();
                _videoFrame.CopyTo(image);
                if (!StartButton.Enabled)
                {
                    await DetectFaces(image.ToMemoryStream(), AnalyzeTypeComboBox.SelectedIndex);
                    VideoFramePictureBox.Image = Image.FromStream(image.ToMemoryStream());
                    VideoFramePictureBox.Refresh();
                    lastDetectionLabel.Text = DateTime.Now.ToString();
                }
            }
        }

        private void VideoFramePictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (var face in _detectedFaces)
            {
                var positionText = RepositionText(face.FaceRectangle.Left, face.FaceRectangle.Top);
                e.Graphics.DrawString($"Gender: {face.FaceAttributes.Gender} / Age: {face.FaceAttributes.Age}", new Font("Arial", 11, FontStyle.Bold), new SolidBrush(Color.Red), positionText.x, positionText.y - 35);
                e.Graphics.DrawString($"Emotion: {face.FaceAttributes.Emotion.ToRankedList().FirstOrDefault().Key}", new Font("Arial", 11, FontStyle.Bold), new SolidBrush(Color.Red), positionText.x, positionText.y - 20);
                e.Graphics.DrawRectangle(_pen, ResizeRectangle(face.FaceRectangle));
            }
        }

        private Rectangle ResizeRectangle(FaceRectangle rect) =>
            new Rectangle((int)(rect.Left * (VideoFramePictureBox.Width * 1.0 / _videoFrame.Width)),
                          (int)(rect.Top * (VideoFramePictureBox.Height * 1.0 / _videoFrame.Height)),
                          (int)(rect.Width * (VideoFramePictureBox.Width * 1.0 / _videoFrame.Width)),
                          (int)(rect.Height * (VideoFramePictureBox.Height * 1.0 / _videoFrame.Height)));

        private (float x, float y) RepositionText(float x, float y)
        {
            try
            {
                return
                ((float)(x * (VideoFramePictureBox.Width * 1.0 / _videoFrame.Width)),
                (float)(y * (VideoFramePictureBox.Height * 1.0 / _videoFrame.Height)));
            }
            catch { return (0, 0); }
        }

        private void SaveFaceData(DetectedFace face)
        {
            var faceData = new
            {
                Source = cameraRadioButton.Checked ? "Câmera" : openFileDialog.FileName.Split('\\').LastOrDefault(),
                FaceID = face.FaceId.ToString(),
                Gender = face.FaceAttributes.Gender.ToString(),
                Age = (int)face.FaceAttributes.Age,
                Emotion = face.FaceAttributes.Emotion.ToRankedList().FirstOrDefault().Key,
                Glasses = face.FaceAttributes.Glasses.ToString(),
                EyeMakeup = face.FaceAttributes.Makeup.EyeMakeup,
                LipMakeup = face.FaceAttributes.Makeup.LipMakeup,
                Beard = face.FaceAttributes.FacialHair.Beard,
                Bald = face.FaceAttributes.Hair.Bald,
                HairColor = face.FaceAttributes.Hair.HairColor.GetTopHairColor()
            };

            var functionUrl = "http://localhost:7071/api/FaceReactionFunction";
            var client = new RestClient(functionUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", Newtonsoft.Json.JsonConvert.SerializeObject(faceData), ParameterType.RequestBody);
            client.ExecuteAsync(request, null);
        }

        private void SaveAdvertisingFaceData(DetectedFace face, string banner)
        {
            var faceData = new
            {
                Banner = banner,
                FaceID = face.FaceId.ToString(),
                Gender = face.FaceAttributes.Gender.ToString(),
                Age = (int)face.FaceAttributes.Age,
                Emotion = face.FaceAttributes.Emotion.ToRankedList().FirstOrDefault().Key,
                Glasses = face.FaceAttributes.Glasses.ToString(),
                Beard = face.FaceAttributes.FacialHair.Beard,
                Bald = face.FaceAttributes.Hair.Bald,
                HairColor = face.FaceAttributes.Hair.HairColor.GetTopHairColor()
            };

            var functionUrl = "http://localhost:7071/api/AdvertisingFaceReactionFunction";
            var client = new RestClient(functionUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", Newtonsoft.Json.JsonConvert.SerializeObject(faceData), ParameterType.RequestBody);
            client.ExecuteAsync(request, null);
        }

        private string GetAdvertisingFacePredominant(DetectedFace face, string banner)
        {
            if (face == null)
                return string.Empty;

            var faceData = new
            {
                Banner = banner,
                Gender = face.FaceAttributes.Gender.ToString(),
                Glasses = face.FaceAttributes.Glasses.ToString(),
            };

            var functionUrl = "http://localhost:7071/api/AdvertisingFaceReactionPredominantFunction";
            var client = new RestClient(functionUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", Newtonsoft.Json.JsonConvert.SerializeObject(faceData), ParameterType.RequestBody);
            var response = client.Post(request);
            return response.Content.Trim('\"');
        }
    }
}
