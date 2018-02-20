using NUnit.Framework;
using Falkonry.Helper.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Falkonry.Tests
{
  [TestFixture()]
  public class TestBackFillProcess
  {
    static string host = System.Environment.GetEnvironmentVariable("FALKONRY_HOST_URL");
    static string token = System.Environment.GetEnvironmentVariable("FALKONRY_TOKEN");
    readonly Falkonry _falkonry = new Falkonry(host, token);
    List<Datastream> _datastreams = new List<Datastream>();
    static string assessmentId = "ck9wk9mnkqypp6";
    static string datastreamId = "pj6qq6r242jgtk";

    EventSource eventSource;

    //Handles live streaming output
    private void EventSource_Message(object sender, EventSource.ServerSentEventArgs e)
    {
      try
      { 
        var falkonryEvent = JsonConvert.DeserializeObject<FalkonryEvent>(e.Data); 
      }
      catch (System.Exception exception)
      {
        // exception in parsing the event
        Assert.AreEqual(exception.Message, null, "Error listening for live data");
      }
    }

    //Handles any error while fetching the live streaming output
    private void EventSource_Error(object sender, EventSource.ServerSentErrorEventArgs e)
    {
      // error connecting to Falkonry service for output streaming
      Assert.AreEqual(e.Exception.Message, null, "Error listening for live data");
    }

    [Test()]
    [Ignore("To be executed when assessment has active model")]
    public void TestBackFillProcessFlow()
    {
      try
      {
        // Got TO Falkonry UI and run a model revision
        // Start backfill process
        var outputStateRequest = new OutputStateRequest();
        outputStateRequest.Datastream = datastreamId;
        List<string> listAssessment = new List<string>();
        listAssessment.Add(assessmentId);
        outputStateRequest.Assessment = listAssessment;
        var outputStateResponse = _falkonry.StartBackfillProcess(outputStateRequest);
        Assert.AreNotEqual(null, outputStateResponse.OutputStateId);
        Assert.AreNotEqual(null, outputStateResponse.InputUrl);
        Assert.AreNotEqual(null, outputStateResponse.OutputUrl);
        Assert.AreEqual(1, outputStateResponse.OutputUrl.Count);



        // Ingest data to backfill process
        var data = "{\"time\":1518379559131,\"unit\":\"UNIT-1\",\"signal1\":24.112649259789087,\"signal2\":7.40800120700027}";
        var options = new SortedDictionary<string, string>();
        var inputstatus = _falkonry.AddInputDataToBackfillProcess(outputStateResponse.OutputStateId, data, options);
        Assert.AreNotEqual(null, inputstatus.Message);


        // Listen output of backfill process
        eventSource = _falkonry.GetOutputDataFromBackfillProcess(outputStateResponse.OutputStateId, assessmentId);

        //On successfull output EventSource_Message will be triggered
        eventSource.Message += EventSource_Message;

        //On any error while getting output, EventSource_Error will be triggered
        eventSource.Error += EventSource_Error;

        //Keep stream open for 60sec
        System.Threading.Thread.Sleep(60000);

        eventSource.Dispose();

        // Stop the back fill process
        _falkonry.StopBackfillProcess(outputStateResponse.OutputStateId);

        _falkonry.DeleteDatastream(datastreamId);
      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error generating backfill output");
      }
    }
  }
}